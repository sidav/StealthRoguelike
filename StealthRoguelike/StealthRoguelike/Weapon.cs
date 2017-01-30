using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Weapon:Item
    {
        public enum WeaponTypes { Melee, Firearm }
        public enum MeleeDamageTypes { Stab, Smash }
        public enum RangedDamageTypes { Piercing }

        new public string DisplayName
        {
            get
            {
                if (isStackable && Quantity > 1)
                    return Quantity.ToString() + " " + name.ToLower() + "s";
                else
                    if (TypeOfWeapon == WeaponTypes.Firearm)
                        return name.ToLower() + " (" + CurrentAmmo + "/" + MaxAmmo + ")";
                    else
                        return name.ToLower();
            }
        }

        public WeaponTypes TypeOfWeapon;
        public MeleeDamageTypes TypeOfMeleeDamage;
        public RangedDamageTypes TypeOfRangedDamage;
        public int mindamage, maxdamage; //melee
        public int Range;
        public int CurrentAmmo = 0, MaxAmmo = 0; //for ranged...
        public int minStrengthRequired = 5;
        public float StrengthFactor = 1;
        public float AgilityFactor = 1;

        public bool targetInRange(int fx, int fy, int tx, int ty)
        {
            int dx = fx - tx;
            int dy = fy - ty;
            int sqdistance = dx * dx + dy * dy;
            if (TypeOfWeapon == WeaponTypes.Melee && sqdistance <= 2)
                return true;
            else
                return (sqdistance <= Range * Range);
        }

        public bool TryReload(Ammunition value)
        {
            if (value.TypeOfAmmunition == Ammunition.AmmoTypes.Bullet && TypeOfWeapon == WeaponTypes.Firearm)
            {
                int toFull = MaxAmmo - CurrentAmmo;
                if (value.Quantity >= toFull)
                {
                    value.Quantity -= toFull;
                    CurrentAmmo = MaxAmmo;
                }
                else
                {
                    CurrentAmmo += value.Quantity;
                    value.Quantity = 0;
                }
                return true;
            }
            return false;
        }


        public Weapon(string WeaponName)
        {
            setProperName(WeaponName);
            switch (name)
            {
                //MELEE:
                case "Dagger":
                    TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Stab;
                    mindamage = 1;
                    maxdamage = 2;
                    break;
                case "Baton":
                    TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
                case "Katar":
                    TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Stab;
                    mindamage = 2;
                    maxdamage = 3;
                    break;

                //FIREARMS:
                case "Revolver":
                    TypeOfWeapon = WeaponTypes.Firearm;
                    TypeOfMeleeDamage = MeleeDamageTypes.Smash;
                    //TypeOf
                    mindamage = 1;
                    maxdamage = 2;
                    MaxAmmo = 6;
                    Range = 6; 
                    break;

                //If you see this ingame, then there is probably a bug somewhere
                default:
                    name = "??Default_Weapon??";
                    TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Smash;
                    mindamage = 1;
                    maxdamage = 1;
                    break;
            }
            //Appearance:
            switch (TypeOfWeapon)
            {
                case WeaponTypes.Melee:
                    Appearance = AllChars.meleeWeaponChar;
                    Color = ConsoleColor.White;
                    Range = 1;
                    break;
                case WeaponTypes.Firearm:
                    Appearance = AllChars.firearmChar;
                    Color = ConsoleColor.DarkCyan;
                    break;
            }
        }

    }
}
