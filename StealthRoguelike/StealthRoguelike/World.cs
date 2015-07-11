using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class World
    {
        char[,] map;
        Unit player;
        public const char wallChar = '#';
        public const char closedDoorChar = '+';
        //public const char openedDoorChar = '''';
        public const char floorChar = '.';
        public const char upstairChar = '<';
        public const char downstairChar = '>';
        public World()
        {
            //generate map
            map = MapGenerator.generateDungeon();

        }
    }
}
