using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Weapon:Item
    {
        public enum weaponTypes { melee }
        public enum damageTypes { stab, smash }

        public string name;
        public weaponTypes type;
        public int mindamage, maxdamage;

        public bool targetInRange(int fx, int fy, int tx, int ty)
        {
            int dx = fx - tx;
            int dy = fy - ty;
            int sqdistance = dx * dx + dy * dy;
            if (type == weaponTypes.melee && sqdistance <= 2)
                return true;
            return false;
        }

        public Weapon(string WeaponName)
        {
            name = WeaponName;
            if (name == "dagger")
            {
                type = weaponTypes.melee;
                mindamage = 1;
                maxdamage = 2;
            }
            if (name == "baton")
            {
                type = weaponTypes.melee;
                mindamage = 1;
                maxdamage = 1;
            }
        }

    }
}
