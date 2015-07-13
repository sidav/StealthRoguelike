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
        public int lookX, lookY;   //heading vector
        public char appearance;
        public bool hasFOV;
        public Unit(int x, int y, char appear, bool fov)
        {
            coordX = x;
            coordY = y;
            appearance = appear;
            hasFOV = fov;
        }

        public void moveForward() //move where this unit looks
        {
            coordX += lookX;
            coordY += lookY;
        }

        public void move(int x, int y) //-1 or 0 or 1 for x and y
        {
            coordX += x;
            coordY += y;
        }

    }
}
