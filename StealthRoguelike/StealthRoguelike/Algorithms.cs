using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
   class Algorithms
    {
        static Random random = new Random();
        const long A = 16807;
        const long M = 2147483647;
        const long C = 65323;
        static long x = 0;

        static int next(int max)
        {
            x = (A * x + C) % M;
            if (max == 0) return 0;
            return (int)x % max;
        }

        public static void setSeed()
        {
            x = DateTime.Now.Ticks;
        }

        public static void setSeed(long seed)
        {
            x = seed;
        }



        public static int getRandomInt(int max)
        {
            int val = next(max);
            return val;
        }
        public static int getRandomInt(int min, int max)
        {
            int val = next(max - min);
            return val + min;
        }

        //public static int getRandomInt(int max) //replace with my own?
        //{
        //    int val = random.Next(max);
        //    return val;
        //}
        //public static int getRandomInt(int min, int max) 
        //{
        //    int val = random.Next(max-min);
        //    return val+min;
        //}



    }
}
