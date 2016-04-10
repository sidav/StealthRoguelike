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

    }

    class InventoryList
    {
        public List<Item> ItemList = new List<Item>();
        public List<char> CharList = new List<char>();
        public List<bool> SelectedList = new List<bool>();
    }
}
