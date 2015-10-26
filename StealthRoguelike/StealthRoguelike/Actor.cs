using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Actor:Unit //it's just like an unit, but with AI
    {

        public enum State {waiting, patrolling, alerted, attacking} //to be expanded...

        public State CurrentState;
        public int StateTimeout;

        public Unit Target; //attack whom? 
        public int WayTargetX, WayTargetY; //where to go for investigation?


        public Actor(string name,int x, int y, char appear):base(name, x,y,appear,true,ConsoleColor.Red)
        {
            CurrentState = State.patrolling;
            StateTimeout = 0;
            WayTargetX = 0;
            WayTargetY = 0;
        }

        public void Draw()
        {
            if (CurrentState != State.alerted && CurrentState != State.attacking)
                base.Draw();
            else
            {
                ConsoleColor tempFore = Console.ForegroundColor;
                ConsoleColor tempBack = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = color;
                Console.SetCursorPosition(this.coordX, this.coordY);
                Console.Write('!');
                Console.ForegroundColor = color;
                Console.BackgroundColor = tempBack;
                if (hasFOV) //draw a thingy that shows this unit's direction
                {
                    drawLookingThingy();
                }
                Console.ForegroundColor = tempFore;
            }
            
        }

        ///////
        //AI!//
        ///////
        const int suddenTurningFrequency = 10;

        void turnToPassable() //turn to random direction which is passable
        {
            do
            {
                lookX = Algorithms.getRandomInt(-1, 2);
                lookY = Algorithms.getRandomInt(-1, 2);
            } while ((lookX == 0 && lookY == 0) || !World.IsPassable(coordX + lookX, coordY + lookY));
            Timing.AddActionTime(5);
        }

        void Check() //Well, does this actor see anything interesting?
        {
            int targetX, targetY;
            //do we see the player?
            targetX = World.player.coordX;
            targetY = World.player.coordY;
            int vectorX = World.player.coordX - coordX;
            int vectorY = World.player.coordY - coordY;
            double distance = Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
            if (distance <= visibilityRadius)
            {
                if (ViewSector.PointIsInSector(coordX, coordY, targetX, targetY, lookX, lookY, ViewAngle))
                    if (World.VisibleLineExist(coordX, coordY, targetX, targetY))
                    {
                        //MORE CODE EXPECTING
                        Log.AddLine(Name + " notices you!");
                        CurrentState = State.attacking;
                        Target = World.player;
                        WayTargetX = Target.coordX;
                        WayTargetY = Target.coordY;
                        return;
                    }
            }
        }

        void DoPatrolling()
        {
            //close door if neccessary
            if (World.TryCloseDoor(coordX - lookX, coordY - lookY))
                return;
            //let's SUDDENLY turn to the random direction, maybe? :D
            if (Algorithms.getRandomInt(suddenTurningFrequency) == 0)
            {
                turnToPassable();
                return;
            }
            //Move forward if there is nothing to do...
            if (TryMoveForward())
                return;
            else //or open door if there is. Otherwise turn to random direction
            {
                if (!World.TryOpenDoor(coordX + lookX, coordY + lookY))
                    turnToPassable();
            }
        }

        void DoAttacking() //not only hit enemy, but also walk toward him first!
        {
            int targetX = WayTargetX - coordX;
            int targetY = WayTargetY - coordY;
            int lookToX = targetX;
            int lookToY = targetY;
            if (targetX != 0)
                lookToX = targetX / Math.Abs(targetX);
            if (targetY != 0)
                lookToY = targetY / Math.Abs(targetY);
            turnToDirection(lookToX, lookToY);
            TryMoveForward();
        }

        public void DoSomething() //main AI method
        {
            if (CurrentState != State.patrolling && CurrentState != State.waiting)
                CurrentState = State.patrolling;
            Check();
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


        }
    }
}
