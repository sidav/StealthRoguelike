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
        static LogMessage[] miniLog = new LogMessage[LogSize]; //log on display
        static string lastLogInput;
        static int equalMessagesCount = 1;

        public static void ClearLog()
        {
            for (int i = 0; i < LogSize; i++)
                miniLog[i] = new LogMessage("Debug: Game log operational");
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
                miniLog[LogSize - 1] = new LogMessage(line);
                DrawMiniLog();
            }
        }

        public static void AddWarning(string line)
        {
            equalMessagesCount = 1;
            lastLogInput = line;
            for (int i = 0; i < LogSize - 1; i++)
                miniLog[i] = miniLog[i + 1];
            miniLog[LogSize - 1] = new LogMessage(line, ConsoleColor.Red);
            DrawMiniLog();
        }

        public static void ReplaceLastLine(string line)
        {
            miniLog[LogSize - 1] = new LogMessage(line);
            DrawMiniLog();
        }

        public static void DrawMiniLog()
        {
            //Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, Program.mapHeight);
            for (int i = 0; i < LogSize; i++)
            {
                Console.ForegroundColor = miniLog[i].TextColor;
                string spaces = " ";
                for (int j = miniLog[i].Text.Length; j < Program.consoleWidth - 2; j++)
                    spaces += " ";
                Console.WriteLine(miniLog[i].Text + spaces);
            }
        }

    }

    class LogMessage
    {
        public string Text = "?EMPTY?";
        public ConsoleColor TextColor = ConsoleColor.Gray;
        public LogMessage(string logtext)
        {
            Text = logtext;
        }
        public LogMessage(string logtext, ConsoleColor color)
        {
            Text = logtext;
            TextColor = color;
        }
    }

}
