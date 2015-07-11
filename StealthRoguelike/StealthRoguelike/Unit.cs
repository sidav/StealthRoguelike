using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Unit
    {
        public int coordX, coordY;
        char appearance;
        bool noFOV;
        public Unit(int x, int y, char appear, bool nofov)
        {
            coordX = x;
            coordY = y;
            appearance = appear;
            noFOV = nofov;
        }

        public void move(int x, int y) //-1 or 0 or 1 for x and y
        {
            coordX += x;
            coordY += y;
        }

    }
}
