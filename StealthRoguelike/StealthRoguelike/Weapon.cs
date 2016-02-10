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

        public weaponTypes type;
        public damageTypes TypeOfDamage;
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
            Name = WeaponName;
            switch (Name)
            {
                case "dagger":
                    type = weaponTypes.melee;
                    TypeOfDamage = damageTypes.stab;
                    mindamage = 1;
                    maxdamage = 2;
                    break;
                case "baton":
                    type = weaponTypes.melee;
                    TypeOfDamage = damageTypes.smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
                default:
                    Name = "Default_Weapon";
                    type = weaponTypes.melee;
                    TypeOfDamage = damageTypes.smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
            }
            if (type == weaponTypes.melee)
            {
                Appearance = ')';
                Color = ConsoleColor.White;
            }

        }

    }
}
