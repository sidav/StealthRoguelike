using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Pawn:Unit //it's just like an unit, but with AI
    {

        public enum State {waiting,patrolling} //to be expanded...

        public State currentState;

        public Pawn(int x, int y, char appear):base(x,y,appear,true,ConsoleColor.Red)
        {
            currentState = State.patrolling;
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

        public void DoSomething() //AI itself
        {
            //if is waiting for something then do nothing, huh
            if (currentState == State.waiting)
                return; //BUT IT'S NOT WHAT WE NEED TO!
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
