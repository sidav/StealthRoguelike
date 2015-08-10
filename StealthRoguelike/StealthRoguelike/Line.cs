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
        static int fromX, fromY, toX, toY, deltax, deltay, err, deltaerr, 
            xmod, ymod;

        public static void Init(int startX, int startY, int endX, int endY)
        {
            fromX = startX;
            fromY = startY;
            toX = endX;
            toY = endY;
            CurX = fromX;
            CurY = fromY;
            deltax = Math.Abs(toX - fromX);
            deltay = Math.Abs(toY - fromY);
            err = 0;
            xmod = 1;
            ymod = 1;
            if (toX < fromX)
                xmod = -1;
            if (toY < fromY)
                ymod = -1;
        }

        public static bool Step() //traverse to next point of the line. 
        {                  //also returns true if the end is reached
            bool endReached = false; 

            if (deltax >= deltay)
            {
                deltaerr = deltay;
                CurX += xmod;
                err += deltaerr;
                if (2 * err >= deltax)
                {
                    CurY += ymod;
                    err -= deltax;
                }
            }
            if (deltax < deltay)
            {
                deltaerr = deltax;
                CurY += ymod;
                err += deltaerr;
                if (2 * err >= deltay)
                {
                    CurX += xmod;
                    err -= deltay;
                }
            }

            // check whenether we reached the end of the line
            if (CurX == toX && CurY == toY) 
                endReached = true;
            return endReached;
        }

    }
}
