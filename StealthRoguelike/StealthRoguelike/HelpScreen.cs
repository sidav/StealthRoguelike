using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class HelpScreen
    {
        public static void DrawHelpScreen()
        {
            int windowWidth = Program.consoleWidth / 5 * 4;
            int windowHeight = Program.consoleHeight / 5 * 4;
            int windowStartX = (Program.consoleWidth - windowWidth) / 2;
            int windowStartY = (Program.consoleHeight - windowHeight) / 2;
            string header = "LIST OF COMMANDS";
            string pressakey = "Press spacebar or enter to continue";
            ConsoleColor oldBckClr = Console.BackgroundColor;
            //draw a window border
            for (int j = windowStartY; j < windowStartY + windowHeight; j++)
                for (int i = windowStartX; i < windowWidth + windowStartX; i++)
                {
                    Console.SetCursorPosition(i, j);
                    if ((i == windowStartX) || (i == windowStartX + windowWidth - 1) || (j == windowStartY) || (j == windowStartY + windowHeight - 1))
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }
                }
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(windowStartX + (windowWidth / 2 - header.Length / 2),windowStartY+1);
            Console.Write(header);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.SetCursorPosition(windowStartX + 2, windowStartY + 3);
            Console.Write("Move with numpad");
            Console.SetCursorPosition(windowStartX + 2, windowStartY + 4);
            Console.Write("Open doors or melee attack enemies by moving toward them");
            Console.SetCursorPosition(windowStartX + 2, windowStartY + 5);
            Console.Write("c - (c)lose a door");
            Console.SetCursorPosition(windowStartX + 2, windowStartY + 6);
            Console.Write("p - (p)eep over corner or a door");
            Console.SetCursorPosition(windowStartX + 2, windowStartY + 7);
            Console.Write("s - (s)trangle enemy");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(windowStartX + (windowWidth / 2 - pressakey.Length / 2), windowStartY + windowHeight - 2);
            Console.Write(pressakey);

            Console.BackgroundColor = oldBckClr;
            ConsoleKeyInfo keyPressed;
            do
                keyPressed = Console.ReadKey(true);
            while (keyPressed.Key != ConsoleKey.Spacebar && keyPressed.Key != ConsoleKey.Escape && keyPressed.Key != ConsoleKey.Enter);
        }
    }
}
