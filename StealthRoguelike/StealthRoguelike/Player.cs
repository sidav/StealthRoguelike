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
            visibilityRadius = 10;
        }

        public void MoveOrOpen(int x, int y) //-1 or 0 or 1 for x and y
        {
            if (World.TryOpenDoor(coordX + x, coordY + y))
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

        //INTERACTION WITH GAMER.

        void closeDoorDialogue()
        {
            Console.SetCursorPosition(0, Program.mapHeight);
            Console.Write("Close door in which direction?");
            ConsoleKeyInfo keyPressed;
            int doorX = coordX, doorY = coordY;
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.NumPad8) //close up
                doorY += -1;
            if (keyPressed.Key == ConsoleKey.NumPad9) //close upper right
            {
                doorX += 1;
                doorY += -1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad6) //close right
                doorX += 1;
            if (keyPressed.Key == ConsoleKey.NumPad3) //close lower right
            {
                doorX += 1;
                doorY += 1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad2) //close down
                doorY += 1;
            if (keyPressed.Key == ConsoleKey.NumPad1) //close lower left
            {
                doorX += -1;
                doorY += 1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad4) //close left
                doorX += -1;
            if (keyPressed.Key == ConsoleKey.NumPad7) //close upper left
            {
                doorX += -1;
                doorY += -1;
            }
            World.TryCloseDoor(doorX, doorY);
            Console.SetCursorPosition(0, Program.mapHeight);
            Console.Write("                                       ");
        }

        void peepDialogue()
        {
            Console.SetCursorPosition(0, Program.mapHeight);
            Console.Write("Peep in which direction?");
            ConsoleKeyInfo keyPressed;
            int peepX = coordX, peepY = coordY;
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.NumPad8) //peep up
                peepY += -1;
            if (keyPressed.Key == ConsoleKey.NumPad9) //peep upper right
            {
                peepX += 1;
                peepY += -1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad6) //peep right
                peepX += 1;
            if (keyPressed.Key == ConsoleKey.NumPad3) //peep lower right
            {
                peepX += 1;
                peepY += 1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad2) //peep down
                peepY += 1;
            if (keyPressed.Key == ConsoleKey.NumPad1) //peep lower left
            {
                peepX += -1;
                peepY += 1;
            }
            if (keyPressed.Key == ConsoleKey.NumPad4) //peep left
                peepX += -1;
            if (keyPressed.Key == ConsoleKey.NumPad7) //peep upper left
            {
                peepX += -1;
                peepY += -1;
            }
            //don't peep through walls anymore! :D
            if (World.IsPassable(peepX, peepY) || World.IsDoor(peepX, peepY))
            {
                World.drawInCircleFOV(peepX, peepY, visibilityRadius);
                World.drawUnitsInCircle(peepX, peepY, visibilityRadius);
                this.Draw();
                Console.SetCursorPosition(0, Program.mapHeight);
                Console.Write("Press any key to continue...     ");
                Console.ReadKey(true);
                Console.SetCursorPosition(0, Program.mapHeight);
                Console.Write("                                 ");
            }
            else
            {
                Console.SetCursorPosition(0, Program.mapHeight);
                Console.Write("Cannot peep through this!");
            }
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
            //ACTIONS
            if (keyPressed.Key == ConsoleKey.C) //close door 
                closeDoorDialogue();
            if (keyPressed.Key == ConsoleKey.P) //peep 
                peepDialogue();
            //TODO!

            //development purposes
            if (keyPressed.Key == ConsoleKey.F1)
            {
                World.drawWorld(-1);
                World.drawUnits(-1);
                Console.ReadKey(true);
            }
        }

    }
}
