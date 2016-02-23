using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class KnockedOutBody:Item
    {
        public int TurnToWakeUp = 0;
        public Unit Knocked;
        public KnockedOutBody(Unit victim)
        {
            Appearance = '&';
            Weight = 25;
            CoordX = victim.CoordX;
            CoordY = victim.CoordY;
            Name = "knocked out " + victim.Name;
            Color = victim.color;
            TurnToWakeUp = victim.Timing.GetCurrentTurn() + victim.KnockedOutTime;
            Knocked = victim;
            Knocked.KnockedOutTime = 0;
        }

        public bool TimeToWakeUp()
        {
            if (TurnToWakeUp <= Knocked.Timing.GetCurrentTurn())
                return true;
            return false;
        }

    }
}
