using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Program
    {
        public const int mapWidth = 80;
        public const int mapHeight = 20;
        public const int consoleWidth = 80;
        public const int consoleHeight = 25;

        static void drawTestMap(char[,] map)
        {
            for (int j = 0; j < mapHeight; j++)
                for (int i = 0; i < mapWidth; i++)
                    if (i > 0 && i < mapWidth - 1 && j > 0 && j < mapHeight - 1)
                        if (map[i - 1, j - 1] != MapGenerator.wallChar || map[i, j - 1] != MapGenerator.wallChar
                            || map[i + 1, j - 1] != MapGenerator.wallChar || map[i - 1, j] != MapGenerator.wallChar ||
                            map[i + 1, j] != MapGenerator.wallChar || map[i - 1, j + 1] != MapGenerator.wallChar
                            || map[i, j + 1] != MapGenerator.wallChar || map[i + 1, j + 1] != MapGenerator.wallChar)
                            Console.Write(map[i, j]);
                        else Console.Write(' ');
                    else Console.Write('#');
        }

        static void drawLine(int fx, int fy, int tx, int ty) //just for algorithm testing
        {
            Line.Init(fx, fy, tx, ty);
            do
            {
                if (Line.CurX < consoleWidth && Line.CurY < consoleHeight && Line.CurX >=0 && Line.CurY >= 0)
                {
                    Console.SetCursorPosition(Line.CurX, Line.CurY);
                    Console.Write('#');
                }
            }
            while (!Line.Step());
        }

        static void drawCrap() //just for visual algorithm testing
        {
            //LINE DRAWING TEST
            for (int i = -consoleWidth; i < consoleWidth; i++)
                for (int j = -consoleHeight; j < consoleHeight; j++)
                    if ((i * i + j * j) <= 25)
                    {
                        Console.SetCursorPosition(0, 0);
                        drawLine(40, 12, 40 - i, 12 - j);
                    }
            Console.ReadKey(true);
            //END OF TEST
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.CursorVisible = false;
            MapGenerator.setParams(mapWidth, mapHeight);
            //drawCrap();
            //char[,] map = MapGenerator.generateDungeon();
            //drawTestMap(map);
            World w = new World();
            w.Loop();
        }
    }
}
