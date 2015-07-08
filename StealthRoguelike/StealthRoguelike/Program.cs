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
            char[,] map = MapGenerator.generateDungeon(80, 25);
            for (int j = 0; j < 25; j++)
                Console.Write(Tools.getRandomInt(100) + " " );
            //for (int j = 0; j < 25; j++)
            //    for (int i = 0; i < 80; i++)
            //        Console.Write(map[i, j]);
            Console.ReadKey();
        }
    }
}
