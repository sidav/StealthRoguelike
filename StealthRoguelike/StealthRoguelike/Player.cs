using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Player:Unit
    {
        static bool isPeeping = false;
        static int lastPeepX;
        static int lastPeepY;

        public Player(int x, int y):base("you",x,y,'@',false,ConsoleColor.Green)
        {
            visibilityRadius = 10;
        }

        public bool seesAsUsual()
        {
            return !isPeeping;
        }

        public void MoveOrOpenOrAttack(int x, int y) //-1 or 0 or 1 for x and y
        {
            if (World.IsDoorPresent(CoordX + x, CoordY + y))
            {
                if (World.TryUnlockDoor(CoordX + x, CoordY + y, Inv.GetAllKeys))
                {
                    Timing.AddActionTime(TimeCost.OpenDoorCost(this));
                    Log.AddLine("You have unlocked the door with your key.");
                    return;
                }
                if (World.TryOpenDoor(CoordX + x, CoordY + y))
                {
                    Timing.AddActionTime(TimeCost.OpenDoorCost(this));
                    Log.AddLine("You opened the door.");
                    return;
                }
            }
            if (World.IsPassable(CoordX + x, CoordY + y))
            {
                CoordX += x;
                CoordY += y;
                Timing.AddActionTime(TimeCost.MoveCost(this));
                if (World.isItemPresent(CoordX, CoordY))
                {
                    List<Item> list = World.getItemListAt(CoordX, CoordY);
                    int numberOfItemsOnFloor = list.Count();
                    if (numberOfItemsOnFloor > 1)
                        Log.AddLine("You see here: " + list[0].DisplayName + " and " + (numberOfItemsOnFloor -1).ToString() + " more items");
                    else
                        Log.AddLine("You see here: " + list[0].DisplayName);
                }
                return;
            }
            if (World.isActorPresent(CoordX + x, CoordY + y))
            {
                Actor attacked = World.getActorAt(CoordX + x, CoordY + y);
                Attack.MeleeAttack(this, attacked);
            }
            else if (!World.IsPassable(CoordX + x, CoordY + y))
                Log.AddLine("Locked! You need a key.");
            //World.Redraw(CoordX-x, CoordY-y);
        }


        //INTERACTION WITH GAMER.

        void seeTheStats()
        {
            string statString = "s " + Stats.Strength.ToString() + "; n " + Stats.Nerve.ToString() +
                "; e " + Stats.Endurance.ToString() + "; a " + Stats.Agility.ToString() + "; k " +
                Stats.Knowledge.ToString();
            _DEBUG.AddDebugMessage(statString);
        }

        void closeDoorDialogue()
        {
            Log.AddLine("Close door in which direction?");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            KeyToVector.ProcessInput(keyPressed);
            int doorX = CoordX+KeyToVector.x, doorY = CoordY + KeyToVector.y;
            if (doorX == CoordX && doorY == CoordY)
            {
                int randomMessageNumber = MyRandom.getRandomInt(3);
                switch (randomMessageNumber)
                {
                    case 0:
                        Log.AddLine("Wow. You costumed like a door this Halloween?");
                        break;
                    case 1:
                        Log.AddLine("You have almost closed yourself, but suddenly remembered that you're not a door.");
                        break;
                    case 2:
                        Log.AddLine("Okay... Try another time");
                        break;
                }
                return;
            }
            if (World.TryCloseDoor(doorX, doorY))
            {
                Timing.AddActionTime(TimeCost.PeepCost(this));
                Log.ReplaceLastLine("You carefully closed the door.");
            }
            else
                Log.ReplaceLastLine("You tried to close this, but something went wrong...");
        }

        void peepDialogue()
        {
            Log.AddLine("Peep in which direction?");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            KeyToVector.ProcessInput(keyPressed);
            int peepX = CoordX + KeyToVector.x, peepY = CoordY + KeyToVector.y;
            if (peepX == CoordX && peepY == CoordY)
            {
                int randomMessageNumber = MyRandom.getRandomInt(3);
                switch (randomMessageNumber)
                {
                    case 0:
                        Log.AddLine("You feel SO introversive for a moment");
                        break;
                    case 1:
                        Log.AddLine("You peep yourself. So interesting");
                        break;
                    case 2:
                        Log.AddLine("If you wanna, hm, look at yourself, get a room, please.");
                        break;
                }
                return;
            }
            //don't peep through walls anymore! :D
            if (World.IsPassable(peepX, peepY) || World.IsDoorPresent(peepX, peepY))
            {
                isPeeping = true;
                lastPeepX = peepX;
                lastPeepY = peepY;
                WorldRendering.drawInCircleFOV(peepX, peepY, visibilityRadius);
                WorldRendering.drawUnitsInCircle(peepX, peepY, visibilityRadius);
                this.Draw();
                Console.ForegroundColor = ConsoleColor.Gray;
                Timing.AddActionTime(TimeCost.CloseDoorCost(this));
                Log.ReplaceLastLine("You carefully peep in that direction... Press space or esc to stop");
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Spacebar || keyPressed.Key == ConsoleKey.Escape)
                {
                    isPeeping = false;
                    Log.ReplaceLastLine("You carefully peep in that direction...");
                }
            }
            else
            {
                Log.ReplaceLastLine("You try to peep through this, but in vain.");
            }
        }

        void ContinuePeep(ConsoleKeyInfo keyPressed)
        {
            Log.AddLine("You continue peeping...");
            WorldRendering.drawInCircleFOV(lastPeepX, lastPeepY, visibilityRadius);
            WorldRendering.drawUnitsInCircle(lastPeepX, lastPeepY, visibilityRadius);
            this.Draw();
            if (keyPressed.Key == ConsoleKey.Spacebar || keyPressed.Key == ConsoleKey.Escape)
            {
                isPeeping = false;
                Log.ReplaceLastLine("You recoiled and looked around.");
            }
        }

        void shootDialogue()
        {
            if (Inv.Wielded.Range == 1)
            {
                Log.AddLine("You can't shoot with the " + Inv.Wielded.DisplayName + "!");
                return;
            }
            Log.AddLine("Which target? (tab - next, f - fire, esc - cancel)");
            bool nextTargetPlease = true;
            while (nextTargetPlease)
            {
                nextTargetPlease = false;
                foreach (Actor currTarget in World.AllActors)
                {
                    if (WorldLOS.VisibleLineExist(CoordX, CoordY, currTarget.CoordX, currTarget.CoordY))
                    {
                        WorldRendering.drawInCircleFOV(CoordX, CoordY, visibilityRadius);
                        WorldRendering.drawUnitsInCircle(CoordX, CoordY, visibilityRadius);
                        this.Draw();
                        currTarget.DrawHighlighted();
                        Console.ForegroundColor = ConsoleColor.Red;
                        WorldRendering.DrawLineNotInclusive(CoordX, CoordY, currTarget.CoordX, currTarget.CoordY, '*');
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                        if (keyPressed.Key == ConsoleKey.Tab)
                        {
                            nextTargetPlease = true;
                            continue;
                        }
                        if (keyPressed.Key == ConsoleKey.Escape)
                        {
                            Log.AddOneFromList(StringFactory.CancelStrings());
                            return;
                        }
                        if (keyPressed.Key == ConsoleKey.F)
                        {
                            Attack.RangedAttack(this, currTarget);
                            return;
                        }
                    }
                }
            }
            Log.ReplaceLastLine("No targets in range!");
        }

        void strangleDialogue()
        {
            Log.AddLine("Grab in which direction?");
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            KeyToVector.ProcessInput(keyPressed);
            int strangleX = CoordX + KeyToVector.x, strangleY = CoordY + KeyToVector.y;
            if (strangleX == CoordX && strangleY == CoordY)
            {
                int randomMessageNumber = MyRandom.getRandomInt(3);
                switch (randomMessageNumber)
                {
                    case 0:
                        Log.AddLine("Wanna strangle yourself huh?");
                        break;
                    case 1:
                        Log.AddLine("Suicide will not help with your mission.");
                        break;
                    case 2:
                        Log.AddLine("If you wanna touch yourself, get a room, please.");
                        break;
                }
                return;
            }
            if (World.isActorPresent(strangleX, strangleY))
            {
                Attack.Strangle(this, World.getActorAt(strangleX, strangleY));
                Timing.AddActionTime(TimeCost.StrangleCost(this));
            }
            else
                Log.AddLine("There's nobody here!");
        }


        public void handleKeys(ConsoleKeyInfo keyPressed)
        {
            if (isPeeping)
            {
                Timing.AddActionTime(TimeCost.ContinuePeepCost(this));
                ContinuePeep(keyPressed);
                return;
            }
            //MOVING/WAITING
            if (keyPressed.Key == ConsoleKey.NumPad5) //skip turn
            {
                Timing.AddActionTime(TimeCost.SkipTurnCost(this));
                return;
            }
            KeyToVector.ProcessInput(keyPressed);
            if (KeyToVector.ProperButtonPressed)
                MoveOrOpenOrAttack(KeyToVector.x, KeyToVector.y);
            //ACTIONS
            if (keyPressed.Key == ConsoleKey.D1)
                seeTheStats();
            if (keyPressed.Key == ConsoleKey.C) //close door 
                closeDoorDialogue();
            if (keyPressed.Key == ConsoleKey.D) //drop an item
                Inv.DropDialogue();
            if (keyPressed.Key == ConsoleKey.F) //fire current weapon
                shootDialogue();
            if (keyPressed.Key == ConsoleKey.G) //grab (pick up) an item
                Inv.PickupDialogue();
            if (keyPressed.Key == ConsoleKey.I) //show inventory
                Inv.ShowInventory();
            if (keyPressed.Key == ConsoleKey.P) //peep 
                peepDialogue();
            if (keyPressed.Key == ConsoleKey.S) //strangle
                strangleDialogue();
            if (keyPressed.Key == ConsoleKey.W) //wield a weapon
                Inv.WieldDialogue();
            if (keyPressed.Key == ConsoleKey.Q) //ready the ammo
                Inv.ReadyAmmoDialogue();
            if (keyPressed.Key == ConsoleKey.R) //reload a gun
                Inv.ReloadDialogue();
            //TODO!
        }

        public void DrawStatusbar()
        {
            Console.SetCursorPosition(0, Program.consoleHeight-2);

            if (Hitpoints > 2*GetMaxHitpoints()/3)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (Hitpoints > GetMaxHitpoints() / 3 && Hitpoints <= 2*GetMaxHitpoints() / 3)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (Hitpoints <= GetMaxHitpoints() / 3)
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("HP: " + Hitpoints.ToString()+"/"+GetMaxHitpoints().ToString() + ";");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" Time: " + Timing.GetCurrentTurn()/10 + "." + Timing.GetCurrentTurn() % 10 + ";"); ///THIS might be not cool...
            Console.Write(" Wielding: " + Inv.Wielded.DisplayName + ";");
            int cx = Console.CursorLeft;
            int cy = Console.CursorTop;
            for (int i = 0; i < Program.consoleWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(cx, cy);
            if (Inv.isCarryingABody())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Carrying a " + Inv.BodyCarrying.DisplayName);
                //_DEBUG.AddDebugMessage("Watafuq? Body is " + Inv.BodyCarrying.DisplayName);
            }
            else Console.Write("                                 ");

            //Text statuses (like, "poisoned", "frightened" etc)
            Console.SetCursorPosition(0, Program.consoleHeight - 1);
            int burden = Inv.getEncumbrance();
            if (burden == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Burdened ");
            }
            if (burden == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Very burdened ");
            }
            if (burden == 3)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("Overburdened ");
                Console.BackgroundColor = ConsoleColor.Black;
            }

        }


    }
}
