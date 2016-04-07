using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Inventory
    {
        public Weapon Wielded;
        public List<Item> backpack = new List<Item>();

        public void AddItemToBackpack(Item picked)
        {
            backpack.Add(picked);
        }
    }
}
