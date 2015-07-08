using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class MapGenerator
    {
        public static char[,] generateDungeon(int mapWidth, int mapHeight)
        {
            char[,] map = new char[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    map[i, j] = '#';
            


            return map;
        }
    }
}
