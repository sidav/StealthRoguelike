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
        public List<Item> Backpack = new List<Item>();

        public Inventory(Unit ownr)
        {
            owner = ownr;
        }

        public void DropBody() //OF COURSE it's not final!
        {
            int dropCoordX = owner.CoordX;
            int dropcoordY = owner.CoordY;
            Item dropped;
            if (BodyCarrying != null)
            {
                if (owner is Player)
                    Log.AddLine("You dropped the " + BodyCarrying.Name + " from your shoulder.");
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
                    //Log.AddDebugMessage("Already carrying!");
                    return false;
                }
                else
                    BodyCarrying = picked;
            }
            else
                Backpack.Add(picked);
            return true;
        }

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
            List<Item> dropped = ItemSelectionMenu("drop", Backpack);
            for (int i = 0; i < dropped.Count; i++)
            {
                if (owner is Player)
                    Log.AddLine("You dropped the " + dropped[i].Name);
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

        public void ShowInventory()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.WriteLine("YOUR ITEMS:");
            Console.WriteLine("\nWielded: " + Wielded.Name);
            if (BodyCarrying != null)
                Console.WriteLine("Carrying body: " + BodyCarrying.Name);
            Console.WriteLine("\n   Backpack: ");
            foreach (Item i in Backpack)
            {
                Console.WriteLine("" + i.Name);
            }
            Console.ReadKey(true);
        }

        public List<Item> ItemSelectionMenu(string ask, List<Item> itemlist)
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
                    Console.WriteLine(selectChar + " " + itemlist[i].Name);
                }
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.NumPad2: cursor++;  break;
                    case ConsoleKey.NumPad8: cursor--;  break;
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
