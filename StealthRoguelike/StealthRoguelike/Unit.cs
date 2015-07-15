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
            if (hasFOV)
            {

            }
            Console.ForegroundColor = temp;
        }

        public void MoveForward() //move where this unit looks
        {
            coordX += lookX;
            coordY += lookY;
        }

        public void MoveOrOpen(int x, int y) //-1 or 0 or 1 for x and y
        {
            if (World.tryOpenDoor(coordX + x, coordY + y))
            {
                World.Redraw(coordX + x, coordY + y);
                return;
            }
            if (World.isWalkable(coordX + x, coordY + y))
            {
                coordX += x;
                coordY += y;
            }
            World.Redraw(coordX-x, coordY-y);
        }

    }
}
