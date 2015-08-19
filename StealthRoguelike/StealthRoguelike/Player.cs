using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Player:Unit
    {
        public Player(int x, int y):base(x,y,'@',false,ConsoleColor.Green)
        {
            visibilityRaduis = 10;
        }

        public void handleKeys(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.NumPad8) //move up
                MoveOrOpen(0, -1);
            if (keyPressed.Key == ConsoleKey.NumPad6) //move right
                MoveOrOpen(1, 0);
            if (keyPressed.Key == ConsoleKey.NumPad2) //move down
                MoveOrOpen(0, 1);
            if (keyPressed.Key == ConsoleKey.NumPad4) //move left
                MoveOrOpen(-1, 0);
            //development purposes
            if (keyPressed.Key == ConsoleKey.F1)
            {
                World.drawWorld(-1);
                Console.ReadKey(true);
            }
        }

    }
}
