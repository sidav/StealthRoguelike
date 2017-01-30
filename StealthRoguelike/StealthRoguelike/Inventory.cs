using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Inventory
    {
        Unit owner;
        public Weapon Wielded;
        public Item BodyCarrying = null; //when you carry a body
        public Ammunition Ready;
        public List<Item> Backpack = new List<Item>();

        public Inventory(Unit ownr)
        {
            owner = ownr;
        }

        public int getInventoryWeight()
        {
            int wght = 0;
            foreach (Item i in Backpack)
            {
                wght += i.GetWeight();
            }
            if (isCarryingABody())
                wght += BodyCarrying.GetWeight();
            return wght;
        }

        public bool isCarryingABody()
        {
            if (BodyCarrying == null)
                return false;
            else return true; 
        }

        public void DropBody() //OF COURSE it's not final!
        {
            int dropCoordX = owner.CoordX;
            int dropcoordY = owner.CoordY;
            Item dropped;
            if (BodyCarrying != null)
            {
                if (owner is Player)
                    Log.AddLine("You dropped the " + BodyCarrying.DisplayName + " from your shoulder.");
                dropped = BodyCarrying;
                dropped.CoordX = dropCoordX;
                dropped.CoordY = dropcoordY;
                owner.Timing.AddActionTime(TimeCost.DropItemCost(dropped));
                World.AllItemsOnFloor.Add(dropped);
                BodyCarrying = null;
            }
        }

        public bool TryPickUpItem(Item picked)
        {
            if (picked is UnconsciousBody || picked is Corpse)
            {
                if (BodyCarrying != null)
                {
                    if (owner is Player)
                        Log.AddLine("You're already carrying a body!");
                    //_DEBUG.AddDebugMessage("Already carrying!");
                    return false;
                }
                else
                    BodyCarrying = picked;
            }
            else
            {
                bool pickedUp = false;
                if (picked.isStackable)
                {
                    foreach (Item itm in Backpack)
                        if (itm.isEqualTo(picked))
                        {
                            itm.Quantity += picked.Quantity;
                            pickedUp = true;
                            break;
                        }
                }
                if (!pickedUp)
                    Backpack.Add(picked);
            }
            return true;
        }

        public bool tryReloadWeapon()
        {
            if (Wielded.TypeOfWeapon == Weapon.WeaponTypes.Firearm && Ready != null)
            {
                if (Wielded.TryReload(Ready))
                    return true;
            }
            return false;
        }

        private int getMaxWeight()
        {
            return (int)(owner.Stats.Strength * 1.5);
        }

        public int getEncumbrance() //returns 0 if not encumbered at all, 1 if encumbered, 2 if heavily encumbered, 3 if stressed
        {   //WORK IN PROGRESS
            int maxwght = getMaxWeight();
            int wght = getInventoryWeight();
            if (wght <= maxwght) return 0;
            if (wght > maxwght && wght <= 1.5 * maxwght) return 1;
            if (wght <= 2 * maxwght) return 2;
            if (wght > 2 * maxwght) return 3;
            return 0;
        }

        /// <summary>
        /// ///
        /// </summary>
        //Player-only interfaces

        public void DropDialogue()
        {
            int dropCoordX = owner.CoordX;
            int dropcoordY = owner.CoordY;
            if (BodyCarrying != null)
            {
                DropBody();
                return;
            }
            List<Item> dropped = MultipleItemSelectionMenu("drop", Backpack);
            for (int i = 0; i < dropped.Count; i++)
            {
                if (owner is Player)
                    Log.AddLine("You dropped the " + dropped[i].DisplayName);
                dropped[i].CoordX = dropCoordX;
                dropped[i].CoordY = dropcoordY;
                owner.Timing.AddActionTime(TimeCost.DropItemCost(dropped[i]));
                World.AllItemsOnFloor.Add(dropped[i]);
                Backpack.Remove(dropped[i]);
                //if (dropped.Count == 1) break;
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void WieldDialogue()
        {
            List<Item> weapsInBackpack = new List<Item>();
            foreach (Item i in Backpack)
                if (i is Weapon)
                    weapsInBackpack.Add(i);
            Weapon toWield = (Weapon)SingleItemSelectionMenu("wield", weapsInBackpack);
            if (toWield != null)
            {
                Backpack.Add(Wielded);
                Wielded = toWield;
                Backpack.Remove(toWield);
                Log.AddLine("You are now wielding the " + Wielded.DisplayName);
            }
        }

        public void ReadyAmmoDialogue()
        {
            List<Item> ammoInBackpack = new List<Item>();
            foreach (Item i in Backpack)
                if (i is Ammunition)
                    ammoInBackpack.Add(i);
            Ammunition toReady = (Ammunition)SingleItemSelectionMenu("ready", ammoInBackpack);
            if (toReady != null)
            {
                if (Ready != null)
                    Backpack.Add(Ready);
                Ready = toReady;
                Backpack.Remove(toReady);
                Log.AddLine("You now have the " + Ready.DisplayName + " as a current ammunition.");
            }
        }

        public void ReloadDialogue()
        {
            if (tryReloadWeapon())
            {
                Log.AddLine("You reload your " + Wielded.DisplayName);
                return;
            }
            if (Ready == null)
            {
                Log.AddLine("You've got no ammo in ready!");
                return;
            }
            Log.AddLine("Wrong ammo in ready!");

        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("YOUR ITEMS");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            if (getEncumbrance() == 1) Console.ForegroundColor = ConsoleColor.Yellow;
            if (getEncumbrance() == 2) Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (getEncumbrance() == 3) Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" (total weight: " + getInventoryWeight() + "/" + getMaxWeight() + "):");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write("\nWielded:");
            Console.WriteLine(" " + Wielded.DisplayName);

            if (Ready != null)
                Console.WriteLine("\nIn ready: " + Ready.DisplayName);

            if (BodyCarrying != null)
                Console.WriteLine("On the shoulder: " + BodyCarrying.DisplayName);
            if (Backpack.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n   Your backpack is empty.   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n   Backpack:   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (Item i in Backpack)
                {
                    Console.WriteLine("" + i.DisplayName);
                }
            }
            Console.ReadKey(true);
        }


        ///ROUTINES.
        public Item SingleItemSelectionMenu(string ask, List<Item> itemlist)
        {
            if (itemlist.Count == 0)
                return null;
            if (itemlist.Count == 1)
                return itemlist[0];
            int cursor = 0;
            ConsoleKeyInfo keyPressed;
            do
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("What item would you want to " + ask + "?");
                for (int i = 0; i < itemlist.Count; i++)
                {
                    if (i == cursor)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(itemlist[i].DisplayName);
                }
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.NumPad2: case ConsoleKey.UpArrow: cursor++; break;
                    case ConsoleKey.NumPad8: case ConsoleKey.DownArrow:cursor--; break;
                    //case ConsoleKey.Spacebar: return itemlist[cursor]; break;
                    //case ConsoleKey.Enter: return itemlist[cursor]; break;
                    case ConsoleKey.Escape: Log.AddLine("Okay, then."); return null; break;
                    default: break;
                }
                if (cursor >= itemlist.Count)
                    cursor = 0;
                if (cursor < 0)
                    cursor = itemlist.Count - 1;
            } while (keyPressed.Key != ConsoleKey.Spacebar && keyPressed.Key != ConsoleKey.Enter);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            return itemlist[cursor];
        }

        public List<Item> MultipleItemSelectionMenu(string ask, List<Item> itemlist)
        {
            if (itemlist.Count <= 1)
                return itemlist;
            int cursor = 0;
            List<Item> selectedItems = new List<Item>();
            List<bool> selectedIndexes = new List<bool>();
            for (int i = 0; i < itemlist.Count; i++)
                selectedIndexes.Add(false);
            string selectChar;
            ConsoleKeyInfo keyPressed;
            do
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("What do you want to " + ask + "?");
                for (int i = 0; i < itemlist.Count; i++)
                {
                    if (i == cursor)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    if (selectedIndexes[i] == false) selectChar = "-";
                    else selectChar = " +";
                    Console.WriteLine(selectChar + " " + itemlist[i].DisplayName);
                }
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.NumPad2: case ConsoleKey.UpArrow: cursor++; break;
                    case ConsoleKey.NumPad8: case ConsoleKey.DownArrow: cursor--; break;
                    case ConsoleKey.Spacebar: selectedIndexes[cursor] = !selectedIndexes[cursor]; break;
                    case ConsoleKey.Escape: Log.AddLine("Okay, then.");  return selectedItems; break;
                    default: break;
                }
                if (cursor >= itemlist.Count)
                    cursor = 0;
                if (cursor < 0)
                    cursor = itemlist.Count-1;
            } while (keyPressed.Key != ConsoleKey.Enter);
            for (int i = 0; i < itemlist.Count; i++)
            {
                if (selectedIndexes[i])
                    selectedItems.Add(itemlist[i]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            return selectedItems;
        }


    }

}
