using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Unit
    {
        public int coordX, coordY; //global coords
        public int lookX = 0, lookY = -1;   //heading vector
        public int visibilityRaduis = 5; //basic visibility distance
        public char appearance;
        public bool hasFOV;
        public ConsoleColor color;

        public Unit(int x, int y, char appear, bool fov, ConsoleColor thiscolor)
        {
            coordX = x;
            coordY = y;
            appearance = appear;
            hasFOV = fov;
            color = thiscolor;
        }

        public Unit(int x, int y, char appear)
        {
            coordX = x;
            coordY = y;
            appearance = appear;
            hasFOV = false;
            color = ConsoleColor.White;
        }

        public void Draw()
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(this.coordX, this.coordY);
            Console.Write(this.appearance);
            if (hasFOV) //draw a thingy that shows this unit's direction
            {
                char thingy = '?';
                if (lookX == 0)
                    thingy = '|';
                if (lookY == 0)
                    thingy = '-';
                if (lookX * lookY == 1)
                    thingy = '\\';
                if (lookX * lookY == -1)
                    thingy = '/';
                Console.SetCursorPosition(coordX + lookX, coordY + lookY);
                Console.Write(thingy);
                //TODO!
            }
            Console.ForegroundColor = temp;
        }

        public void MoveForward() //move where this unit looks
        {
            coordX += lookX;
            coordY += lookY;
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
            //close door if neccessary
            if (World.TryCloseDoor(coordX-lookX, coordY-lookY))
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
