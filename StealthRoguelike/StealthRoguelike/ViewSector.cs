using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class ViewSector
    {
        public static bool _PointIsInSectorTEST(int x0, int y0, int x1, int y1, int viewX, int viewY, int viewAngle)
        {
            double halfViewAngle = (viewAngle / 2) * Math.PI / 180; //Translates this into radians!
            Console.WriteLine("Half view angle = " + (halfViewAngle*180/Math.PI).ToString());
            int x = x1 - x0;
            int y = y1 - y0;
            Console.WriteLine("translated x = " + x.ToString() + ", y = " + y.ToString());
            double lookingAngle = Math.Atan2(viewY, viewX);
            Console.WriteLine("Looking angles: from " + ((lookingAngle-halfViewAngle) * 180 / Math.PI).ToString() + " through " + (lookingAngle * 180 / Math.PI).ToString() + " to " + ((lookingAngle + halfViewAngle) * 180 / Math.PI).ToString());
            double targetAngle = Math.Atan2(y, x);
            Console.WriteLine("Target angle = " + (targetAngle* 180 / Math.PI).ToString());
            if (targetAngle >= lookingAngle - halfViewAngle && targetAngle <= lookingAngle + halfViewAngle)
                return true;
            return false;
        }

        public static bool PointIsInSector(int x0, int y0, int x1, int y1, int viewX, int viewY, int viewAngle)
        {
            double halfViewAngle = (viewAngle / 2) * Math.PI / 180; //Translates this into radians!
            int x = x1 - x0;
            int y = y1 - y0;
            double lookingAngle = Math.Atan2(viewY, viewX);
            double targetAngle = Math.Atan2(y, x);
            if (targetAngle >= lookingAngle - halfViewAngle && targetAngle <= lookingAngle + halfViewAngle)
                return true;
            return false;
        }
    }
}
