using System;
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
            Program.ClearScreen();
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

            ConsoleKeyInfo keyPressed;
            do
                keyPressed = Console.ReadKey(true);
            while (keyPressed.Key != ConsoleKey.Spacebar && keyPressed.Key != ConsoleKey.Escape);
        }
    }
}
