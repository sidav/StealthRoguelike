using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Log //Game log lines would be there!
    {

        const int LogSize = Program.LogSize;
        static string[] miniLog = new string[LogSize]; //log on display
        static string lastLogInput;
        static int equalMessagesCount = 1;

        public static void ClearLog()
        {
            for (int i = 0; i < LogSize; i++)
                miniLog[i] = "Debug: Game log operational";
        }

        public static void AddLine(string line)
        {
            if (line == lastLogInput)
            {
                equalMessagesCount++;
                ReplaceLastLine(line + " (x" + equalMessagesCount.ToString() + ")");
            }
            else
            {
                equalMessagesCount = 1;
                lastLogInput = line;
                for (int i = 0; i < LogSize - 1; i++)
                    miniLog[i] = miniLog[i + 1];
                miniLog[LogSize - 1] = line;
                DrawMiniLog();
            }
        }

        public static void ReplaceLastLine(string line)
        {
            miniLog[LogSize - 1] = line;
            DrawMiniLog();
        }

        public static void DrawMiniLog()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, Program.mapHeight);
            for (int i = 0; i < LogSize; i++)
            {
                string spaces = " ";
                for (int j = miniLog[i].Length; j < Program.consoleWidth - 2; j++)
                    spaces += " ";
                Console.WriteLine(miniLog[i] + spaces);
            }
        }

    }
}
