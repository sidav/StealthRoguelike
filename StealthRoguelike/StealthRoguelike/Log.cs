using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Log //Game log lines would be there!
    {
        static string[] miniLog = new string[Program.LogSize]; //log on display

        public static void clearLog()
        {
            for (int i = 0; i < Program.LogSize; i++)
                miniLog[i] = " ";
        }

        public static void AddLine(string line)
        {
            for (int i = 0; i < Program.LogSize - 1; i++)
                miniLog[i] = miniLog[i + 1];
            miniLog[Program.LogSize - 1] = line;
        }
    }
}
