using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Program
    {
        static void drawTestMap(char[,] map)
        {
            for (int j = 0; j < 25; j++)
                for (int i = 0; i < 80; i++)
                    //if (i != 79 || j != 24)
                    if (i > 0 && i < 80 - 1 && j > 0 && j < 25 - 1)
                        if (map[i - 1, j - 1] != '#' || map[i, j - 1] != '#'
                            || map[i + 1, j - 1] != '#' || map[i - 1, j] != '#' ||
                            map[i + 1, j] != '#' || map[i - 1, j + 1] != '#'
                            || map[i, j + 1] != '#' || map[i + 1, j + 1] != '#')
                            Console.Write(map[i, j]);
                        else Console.Write(' ');
                    else Console.Write(map[i, j]);
        }

        static void Main(string[] args)
        {
            Console.SetBufferSize(80, 26);
            Console.SetWindowSize(80, 26);
            Console.CursorVisible = false;
            MapGenerator.setParams(80, 25);
            char[,] map = MapGenerator.generateDungeon();
            drawTestMap(map);
            Console.ReadKey();
        }
    }
}
