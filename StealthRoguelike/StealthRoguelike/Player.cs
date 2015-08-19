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

        public void MoveOrOpen(int x, int y) //-1 or 0 or 1 for x and y
        {
            if (World.tryOpenDoor(coordX + x, coordY + y))
            {
                //World.Redraw(coordX + x, coordY + y);
                return;
            }
            if (World.IsPassable(coordX + x, coordY + y))
            {
                coordX += x;
                coordY += y;
            }
            //World.Redraw(coordX-x, coordY-y);
        }

        public void handleKeys(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.NumPad8) //move up
                MoveOrOpen(0, -1);
            if (keyPressed.Key == ConsoleKey.NumPad9) //move upper right
                MoveOrOpen(1, -1);
            if (keyPressed.Key == ConsoleKey.NumPad6) //move right
                MoveOrOpen(1, 0);
            if (keyPressed.Key == ConsoleKey.NumPad3) //move lower right
                MoveOrOpen(1, 1);
            if (keyPressed.Key == ConsoleKey.NumPad2) //move down
                MoveOrOpen(0, 1);
            if (keyPressed.Key == ConsoleKey.NumPad1) //move lower left
                MoveOrOpen(-1, 1);
            if (keyPressed.Key == ConsoleKey.NumPad4) //move left
                MoveOrOpen(-1, 0);
            if (keyPressed.Key == ConsoleKey.NumPad7) //move upper left
                MoveOrOpen(-1, -1);
            if (keyPressed.Key == ConsoleKey.NumPad5) //skip turn
            {
                //nothing (yet?)
            }
            //development purposes
            if (keyPressed.Key == ConsoleKey.F1)
            {
                World.drawWorld(-1);
                Console.ReadKey(true);
            }
        }

    }
}
