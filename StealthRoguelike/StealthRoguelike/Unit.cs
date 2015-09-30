using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Unit
    {
        public string UnitName;
        public int coordX, coordY; //global coords
        public int lookX = 0, lookY = -1;   //heading vector
        public int visibilityRadius = 5; //basic visibility distance
        public char appearance;
        public bool hasFOV;
        public ConsoleColor color;

        public Unit(string name, int x, int y, char appear, bool fov, ConsoleColor thiscolor)
        {
            UnitName = name;
            coordX = x;
            coordY = y;
            appearance = appear;
            hasFOV = fov;
            color = thiscolor;
        }

        public Unit(string name, int x, int y, char appear)
        {
            UnitName = name;
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

    }
}
