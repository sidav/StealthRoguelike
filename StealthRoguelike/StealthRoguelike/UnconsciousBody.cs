using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class UnconsciousBody:Item
    {
        public int TurnToWakeUp = 0;
        public Unit Knocked;
        public UnconsciousBody(Unit victim)
        {
            Appearance = '&';
            weight = 25;
            CoordX = victim.CoordX;
            CoordY = victim.CoordY;
            name = "unconscious " + victim.Name;
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
