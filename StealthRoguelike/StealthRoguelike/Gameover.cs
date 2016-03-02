﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Gameover //Game over screen and statistics
    {
        public static int KnockedOutEnemies = 0;
        public static int KilledEnemies = 0;
        public static string KilledBy = "bad debug";

        public static void ShowGameoverScreen()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Program.consoleWidth * Program.consoleHeight - 1; i++)
                Console.Write(' ');
            string youdeadstring = "------- YOU DIED -------";
            Console.SetCursorPosition(Program.consoleWidth/2-(youdeadstring.Length/2), 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(youdeadstring);
            string youkilledby = "Killed by " + KilledBy;
            Console.SetCursorPosition(Program.consoleWidth/2-(youkilledby.Length/2), 6);
            Console.Write(youkilledby);
            string pressspacebar = "Press spacebar";
            Console.SetCursorPosition(Program.consoleWidth / 2 - (pressspacebar.Length / 2), Program.consoleHeight-2);
            Console.Write(pressspacebar);
        }
    }
}
