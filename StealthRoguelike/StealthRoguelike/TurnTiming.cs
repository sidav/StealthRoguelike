using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class TurnTiming
    {
        public static int CurrentTurn = 0;

        int NextActionTurn = 0;

        public void AddActionTime(int time)
        {
            NextActionTurn += time;
        }

        public int GetCurrentTurn()
        {
            return CurrentTurn;
        }

        public bool IsTimeToAct()
        {
            if (NextActionTurn < CurrentTurn)
                NextActionTurn = CurrentTurn;
            if (NextActionTurn == CurrentTurn)
                return true;
            return false;
        }

        public static void Tick()
        {
            CurrentTurn++;
        }
    }
}
