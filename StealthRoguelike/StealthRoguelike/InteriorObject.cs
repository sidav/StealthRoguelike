using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class InteriorObject //Furniture or smth
    {
        public int CoordX, CoordY;
        public bool IsSneakable = false, IsPassable = false, IsVisionBlocking = true;
        public string Name = "Base interion object";
        public char Appearance = '%';
        public ConsoleColor color;

        public InteriorObject(int x, int y)
        {
            CoordX = x;
            CoordY = y;
        }
    }
}
