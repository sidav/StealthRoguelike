using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Line
    {
        public static int CurX = 0, CurY = 0;
        static bool lineEnded = true;
        static int fromX, fromY, toX, toY;
        static void Init(int startX, int startY, int endX, int endY)
        {
            fromX = startX;
            fromY = startY;
            toX = endX;
            toY = endY;
        }
        static bool Step() //traverse to next point of the line. 
        {                  //also returns true if the end is reached
            bool endReached = false;


            return endReached;
        }

    }
}
