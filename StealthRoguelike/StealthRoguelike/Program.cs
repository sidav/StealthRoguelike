using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(80, 25);
            Console.CursorVisible = false;
            MapGenerator.setParams(80, 25);
            char[,] map = MapGenerator.generateDungeon();
            for (int j = 0; j < 25; j++)
                for (int i = 0; i < 80; i++)
                    Console.Write(map[i, j]);
            Console.ReadKey();
        }
    }
}
