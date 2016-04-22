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
            weight = 25;
            CoordX = deadman.CoordX;
            CoordY = deadman.CoordY;
            Name = deadman.Name + " corpse";
            Color = deadman.color;
        }
    }
}
