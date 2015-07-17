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
        public static int getRandomInt(int max) //replace with my own?
        {
            int val = random.Next(max);
            return val;
        }
        public static int getRandomInt(int min, int max) 
        {
            int val = random.Next(max-min);
            return val+min;
        }



    }
}
