using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class ViewSector
    {
        static double VectorToAngle(int x, int y)
        {
            double angle = Math.Atan2(y, x);
            return angle;
        }
        public static bool PointIsInSector(int x0, int y0, int x1, int y1, int viewX, int viewY, int viewAngle)
        {
            double halfViewAngle = (viewAngle / 2) * Math.PI / 180; //Translates this into radians!
            int x = x1 - x0;
            int y = y1 - y0;
            double lookingAngle = Math.Atan2(viewY, viewX);
            double targetAngle = Math.Atan2(y, x);
            if (targetAngle <= lookingAngle+halfViewAngle && targetAngle >= lookingAngle-targetAngle)
                return true;
            return false;
        }
    }
}
