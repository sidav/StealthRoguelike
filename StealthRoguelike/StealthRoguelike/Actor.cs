using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Actor:Unit //it's just like an unit, but with AI
    {

        public enum State {waiting,patrolling, alerted} //to be expanded...

        public State currentState;


        public Actor(string name,int x, int y, char appear):base(name, x,y,appear,true,ConsoleColor.Red)
        {
            currentState = State.patrolling;
        }

        public void Draw()
        {
            if (currentState != State.alerted)
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
                        currentState = State.alerted;
                        return;
                    }
            }
        }

        public void DoSomething() //AI itself
        {
            currentState = State.patrolling; //DELETE THIS
            Check();
            //if is waiting for something then do nothing, huh
            if (currentState == State.waiting)
                return; //BUT IT'S NOT WHAT WE NEED TO!
            if (currentState == State.patrolling)
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
                if (World.IsPassable(coordX + lookX, coordY + lookY))
                {
                    MoveForward();
                    return;
                }
                else //or open door if there is. Otherwise turn to random direction
                {
                    if (!World.TryOpenDoor(coordX + lookX, coordY + lookY))
                        turnToPassable();
                }
            }

        }
    }
}
