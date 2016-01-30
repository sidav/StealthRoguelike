using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Corpse:Item
    {
        public Corpse(Unit deadman)
        {
            Appearance = '&';
            Weight = 25;
            CoordX = deadman.coordX;
            CoordY = deadman.coordY;
            Name = deadman.Name + " corpse";
            Color = ConsoleColor.Red;
        }
    }
}
