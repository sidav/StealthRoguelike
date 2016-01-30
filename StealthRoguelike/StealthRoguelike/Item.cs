using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Item //Any thing that can be picked up.
    {
        public string Name;
        public int Weight = 1;
        public int CoordX, CoordY;
        public char Appearance;
        public ConsoleColor Color;

        public void Draw()
        {
            ConsoleColor currcolor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(CoordX, CoordY);
            Console.Write(Appearance);
            Console.ForegroundColor = currcolor;
        }
    }
}
