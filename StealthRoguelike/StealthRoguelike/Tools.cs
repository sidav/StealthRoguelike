﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
   class Tools
    {
        static Random random = new Random();
        public static int getRandomInt(int max) //replace with my own?
        {
            int val = random.Next(max);
            return val;
        }
    }
}
