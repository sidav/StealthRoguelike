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
        public List<Item> backpack = new List<Item>();

        public Inventory(Unit ownr)
        {
            owner = ownr;
        }

        public void DropBody() //OF COURSE it's not final!
        {
            int dropCoordX = owner.CoordX;
            int dropcoordY = owner.CoordY;
            Item dropping;
            if (BodyCarrying != null)
            {
                if (owner is Player)
                    Log.AddLine("You dropped the " + BodyCarrying.Name + " from your shoulder.");

                dropping = BodyCarrying;
                dropping.CoordX = dropCoordX;
                dropping.CoordY = dropcoordY;
                World.AllItemsOnFloor.Add(dropping);
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
                backpack.Add(picked);
            return true;
        }

        //Player-only interfaces

        public void DropDialogue()
        {
            if (BodyCarrying != null)
            {
                DropBody();
                return;
            }
            //if ()
        }

        public void PickupMenu(List<Item> pickuplist)
        {
            int cursor = 0;
            List<bool> selected = new List<bool>();
            for (int i = 0; i < pickuplist.Count; i++)
                selected.Add(false);
            string selectChar;
            ConsoleKeyInfo keyPressed;
            do
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                Console.WriteLine("What do you want to pick up?");
                for (int i = 0; i < pickuplist.Count; i++)
                {
                    if (i == cursor)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;
                    if (selected[i] == false) selectChar = "-";
                    else selectChar = " +";
                    Console.WriteLine(selectChar + " " + pickuplist[i].Name);
                }
                keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.NumPad2: cursor++;  break;
                    case ConsoleKey.NumPad8: cursor--;  break;
                    case ConsoleKey.Spacebar: selected[cursor] = !selected[cursor]; break;
                    default: break;
                }
                if (cursor >= pickuplist.Count)
                    cursor = 0;
                if (cursor < 0)
                    cursor = pickuplist.Count-1;
            } while (keyPressed.Key != ConsoleKey.Enter);
        }


    }

}
