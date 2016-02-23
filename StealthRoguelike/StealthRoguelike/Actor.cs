using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Actor:Unit //it's just like an unit, but with AI
    {

        public enum State {waiting, patrolling, investigating, alerted, attacking} //to be expanded...

        public State CurrentState;
        public int StateTimeout;

        public Unit Target; //attack whom? 
        public int DestinationX, DestinationY; //where to go for investigation?


        public Actor(string name,int x, int y, char appear):base(name, x,y,appear,true,ConsoleColor.Red)
        {
            CurrentState = State.patrolling;
            StateTimeout = 0;
            DestinationX = 0;
            DestinationY = 0;
        }

        protected char getAppearance()
        {
            if (CurrentState == State.alerted)
            {
                return '!';
            }
            if (CurrentState == State.attacking)
            {
                return '!';
            }
            if (CurrentState == State.investigating)
            {
                return '?';
            }
            return this.appearance;
        }

        public new void Draw()
        {
            if (getAppearance() == appearance) //i.e. actor isn't threatened
                base.Draw();
            else
            {
                ConsoleColor tempFore = Console.ForegroundColor;
                ConsoleColor tempBack = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = color;
                Console.SetCursorPosition(this.CoordX, this.CoordY);
                Console.Write(getAppearance());
                Console.ForegroundColor = color;
                Console.BackgroundColor = tempBack;
                if (hasFOV) //draw a thingy that shows this unit's direction
                {
                    drawLookingThingy();
                }
                Console.ForegroundColor = tempFore;
            }

        }

        public override bool IsUnaware()
        {
            if (CurrentState == State.attacking)
                return false;
            return true;
        }

        ///////////////////////////////////////////////////////////
        ///////
        //AI!//
        ///////
        const int suddenTurningFrequency = 10;

        void addStateTimeout(int value)
        {
            StateTimeout = Timing.GetCurrentTurn() + value;
        }

        bool moveToDestination() //returns true if this actor is already at his destination
        {
            if (CoordX == DestinationX && CoordY == DestinationY)
                return true;
            else
            {
                int targetX = DestinationX - CoordX;
                int targetY = DestinationY - CoordY;
                int lookToX = targetX;
                int lookToY = targetY;
                if (targetX != 0)
                    lookToX = targetX / Math.Abs(targetX);
                if (targetY != 0)
                    lookToY = targetY / Math.Abs(targetY);
                turnToDirection(lookToX, lookToY);
                moveForwardOrOpen();
                return false;
            }
        }

        void turnToRandomPassableDirection() //turn to random direction which is passable
        {
            do
            {
                lookX = Algorithms.getRandomInt(-1, 2);
                lookY = Algorithms.getRandomInt(-1, 2);
            } while ((lookX == 0 && lookY == 0) || !World.IsPassable(CoordX + lookX, CoordY + lookY));
            Timing.AddActionTime(5);
        }

        bool ActorSeesTheTarget()  //do we see the player?
        {
            Target = World.player;
            int targetX, targetY;
            targetX = Target.CoordX;
            targetY = Target.CoordY;
            int vectorX = Target.CoordX - CoordX;
            int vectorY = Target.CoordY - CoordY;
            double distance = Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
            if (distance <= visibilityRadius)
            {
                if (ViewSector.PointIsInSector(CoordX, CoordY, targetX, targetY, lookX, lookY, ViewAngle))
                    if (World.VisibleLineExist(CoordX, CoordY, targetX, targetY))
                        return true;
            }
            return false;
        }

        void Check() //Well, does this actor see anything interesting?
        {
            if (ActorSeesTheTarget())
            { 
                //MORE CODE EXPECTING
                Log.AddLine(Name + " notices you!");
                CurrentState = State.attacking;
                DestinationX = Target.CoordX;
                DestinationY = Target.CoordY;
                addStateTimeout(1);
                return;
            }
            else if (CurrentState == State.attacking)
            {
                CurrentState = State.investigating;
                DestinationX = Target.CoordX;
                DestinationY = Target.CoordY;
                addStateTimeout(150);
                return;
            }
        }

        void DoPatrolling()
        {
            //close door if neccessary
            if (World.TryCloseDoor(CoordX - lookX, CoordY - lookY))
                return;
            //let's SUDDENLY turn to the random direction, maybe? :D
            if (Algorithms.getRandomInt(suddenTurningFrequency) == 0)
            {
                turnToRandomPassableDirection();
                return;
            }
            //Move forward if there is nothing to do...
            if (TryMoveForward())
                return;
            else //or open door if there is. Otherwise turn to random direction
            {
                if (!World.TryOpenDoor(CoordX + lookX, CoordY + lookY))
                    turnToRandomPassableDirection();
            }
        }

        void DoAttacking() //not only hit enemy, but also walk toward him first!
        {
            if (Wielded.targetInRange(CoordX, CoordY, DestinationX, DestinationY))
            {
                Attack.dealDamage(this, Target);
                Timing.AddActionTime(10);
            }            
            else
                moveToDestination();
        }

        void DoInvestigating() //move to "last seen position"
        {
            if (moveToDestination())
            {
                turnToRandomPassableDirection();
                Log.AddLine("Where are you? Don't hide, it's useless!");
            }
        }

        public void DoSomething() //main AI method
        {
            Check();
            if (Timing.GetCurrentTurn() > StateTimeout)
                CurrentState = State.patrolling;
            //if is waiting for something then do nothing, huh
            if (CurrentState == State.waiting)
                return;
            if (CurrentState == State.patrolling)
            {
                DoPatrolling();
            }
            if (CurrentState == State.attacking)
            {
                DoAttacking();
            }
            if (CurrentState == State.investigating)
            {
                DoInvestigating();
            }

        }
    }
}
