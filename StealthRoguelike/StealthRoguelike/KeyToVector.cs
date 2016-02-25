using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class KeyToVector //transforms numpad key to appropriate direction vector
    {
        public static int x = 0, y = 0; //vector coords
        public static bool ProperButtonPressed = true;

        public static void ProcessInput(ConsoleKeyInfo keyPressed)
        {
            ConsoleKey key = keyPressed.Key;
            if (key == ConsoleKey.NumPad8) //move up
            {
                x = 0;
                y = -1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad9) //move upper right
            {
                x = 1;
                y = -1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad6) //move right
            {
                x = 1;
                y = 0;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad3) //move lower right
            {
                x = 1;
                y = 1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad2) //move down
            {
                x = 0;
                y = 1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad1) //move lower left
            {
                x = -1;
                y = 1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad4) //move left
            {
                x = -1;
                y = 0;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad7) //move upper left
            {
                x = -1;
                y = -1;
                ProperButtonPressed = true;
                return;
            }
            if (key == ConsoleKey.NumPad5) //move upper left
            {
                x = 0;
                y = 0;
                ProperButtonPressed = true;
                return;
            }
            ProperButtonPressed = false;
        }
    }
}
