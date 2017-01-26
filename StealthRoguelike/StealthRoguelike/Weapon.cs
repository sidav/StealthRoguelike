using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Weapon:Item
    {
        public enum weaponTypes { Melee }
        public enum damageTypes { Stab, Smash }

        public weaponTypes type;
        public damageTypes TypeOfDamage;
        public int mindamage, maxdamage;
        public int minStrengthRequired = 5;
        public float StrengthFactor = 1;
        public float AgilityFactor = 1;

        public bool targetInRange(int fx, int fy, int tx, int ty)
        {
            int dx = fx - tx;
            int dy = fy - ty;
            int sqdistance = dx * dx + dy * dy;
            if (type == weaponTypes.Melee && sqdistance <= 2)
                return true;
            return false;
        }


        public Weapon(string WeaponName)
        {
            name = WeaponName;
            switch (name)
            {
                case "dagger":
                    type = weaponTypes.Melee;
                    TypeOfDamage = damageTypes.Stab;
                    mindamage = 1;
                    maxdamage = 2;
                    break;
                case "baton":
                    type = weaponTypes.Melee;
                    TypeOfDamage = damageTypes.Smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
                case "katar":
                    type = weaponTypes.Melee;
                    TypeOfDamage = damageTypes.Stab;
                    mindamage = 2;
                    maxdamage = 3;
                    break;
                //just for lulz
                case "concept-art drawing":
                    type = weaponTypes.Melee;
                    TypeOfDamage = damageTypes.Smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
                //If you see this ingame, then there is probaby a bug somewhere
                default:
                    name = "Default_Weapon";
                    type = weaponTypes.Melee;
                    TypeOfDamage = damageTypes.Smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
            }
            if (type == weaponTypes.Melee)
            {
                Appearance = ')';
                Color = ConsoleColor.White;
            }

        }

    }
}
