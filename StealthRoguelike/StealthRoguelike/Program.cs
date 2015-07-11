using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Program
    {
        const int mapWidth = 80;
        const int mapHeight = 25;

        static void drawTestMap(char[,] map)
        {
            for (int j = 0; j < mapHeight; j++)
                for (int i = 0; i < mapWidth; i++)
                    //if (i != 79 || j != 24)
                    if (i > 0 && i < mapWidth - 1 && j > 0 && j < mapHeight - 1)
                        if (map[i - 1, j - 1] != MapGenerator.wallChar || map[i, j - 1] != MapGenerator.wallChar
                            || map[i + 1, j - 1] != MapGenerator.wallChar || map[i - 1, j] != MapGenerator.wallChar ||
                            map[i + 1, j] != MapGenerator.wallChar || map[i - 1, j + 1] != MapGenerator.wallChar
                            || map[i, j + 1] != MapGenerator.wallChar || map[i + 1, j + 1] != MapGenerator.wallChar)
                            Console.Write(map[i, j]);
                        else Console.Write(' ');
                    else Console.Write('#');
        }

        static void Main(string[] args)
        {
            Console.SetBufferSize(80, 26);
            Console.SetWindowSize(80, 26);
            Console.CursorVisible = false;
            MapGenerator.setParams(mapWidth, mapHeight);
            char[,] map = MapGenerator.generateDungeon();
            drawTestMap(map);
            Console.ReadKey();
        }
    }
}
