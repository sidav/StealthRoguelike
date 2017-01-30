using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Weapon:Item
    {
        //public enum WeaponTypes { Melee, Firearm }
        public enum MeleeDamageTypes { Stab, Smash }
        public enum RangedDamageTypes { NoRangedDamage, Piercing }

        new public string DisplayName
        {
            get
            {
                if (isStackable && Quantity > 1)
                    return Quantity.ToString() + " " + name.ToLower() + "s";
                else
                    if (IsReloadable)
                        return name.ToLower() + " (" + CurrentAmmo + "/" + MaxAmmo + ")";
                    else
                        return name.ToLower();
            }
        }

        public bool IsReloadable
        {
            get
            {
                return (MaxAmmo > 0);
            }
        }

        public bool isShooting
        {
            get
            {
                return (Range > 1);
            }
        }

        //public WeaponTypes TypeOfWeapon;
        public MeleeDamageTypes TypeOfMeleeDamage;
        public RangedDamageTypes TypeOfRangedDamage;
        public int MinMeleeDamage = 1, MaxMeleeDamage = 1; //melee
        public int MinRangedDamage = 0, MaxRangedDamage = 0;
        public int Range = 1;
        public int AmmoConsuming = 1;
        public int CurrentAmmo = 0, MaxAmmo = 0; //for ranged...
        public int minStrengthRequired = 5;
        public float StrengthFactor = 1;
        public float AgilityFactor = 1;

        public bool targetInEffectiveShootingRange(int fx, int fy, int tx, int ty)
        {
            int dx = fx - tx;
            int dy = fy - ty;
            int sqdistance = dx * dx + dy * dy;
            //if (Range == 1 && sqdistance <= 2) //This is so dirty.
            //    return true;
            //else
            return (Range > 1 && sqdistance <= Range * Range);
        }

        public bool targetInMeleeRange(int fx, int fy, int tx, int ty)
        {
            int dx = fx - tx;
            int dy = fy - ty;
            int sqdistance = dx * dx + dy * dy;
            if (sqdistance <= 2) //This is so dirty.
                return true;
            return false;
        }

        public bool TryReload(Ammunition value)
        {
            if (value.TypeOfAmmunition == Ammunition.AmmoTypes.Bullet && IsReloadable)
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

        public bool TryConsumeAmmo()
        {
            if (CurrentAmmo >= AmmoConsuming)
            {
                CurrentAmmo -= AmmoConsuming;
                return true;
            }
            return false;
        }


        public Weapon(string WeaponName)
        {
            setProperName(WeaponName);
            TypeOfRangedDamage = RangedDamageTypes.NoRangedDamage;
            TypeOfMeleeDamage = MeleeDamageTypes.Smash;
            switch (name)
            {
                //MELEE:
                case "Dagger":
                    //TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Stab;
                    MinMeleeDamage = 1;
                    MaxMeleeDamage = 2;
                    break;
                case "Baton":
                    //TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Smash;
                    MinMeleeDamage = 1;
                    MaxMeleeDamage = 1;
                    break;
                case "Katar":
                    //TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Stab;
                    MinMeleeDamage = 2;
                    MaxMeleeDamage = 3;
                    break;

                //FIREARMS:
                case "Revolver":
                    TypeOfRangedDamage = RangedDamageTypes.Piercing;
                    MinMeleeDamage = 1;
                    MaxMeleeDamage = 2;
                    MaxAmmo = 6;
                    Range = 6; 
                    break;
                case "Pistolknife":
                    TypeOfMeleeDamage = MeleeDamageTypes.Stab;
                    TypeOfRangedDamage = RangedDamageTypes.Piercing;
                    MinMeleeDamage = 1;
                    MaxMeleeDamage = 3;
                    MaxAmmo = 1;
                    Range = 4;
                    break;

                //If you see this ingame, then there is probably a bug somewhere
                default:
                    name = "??Error_Weapon??";
                    //TypeOfWeapon = WeaponTypes.Melee;
                    TypeOfMeleeDamage = MeleeDamageTypes.Smash;
                    MinMeleeDamage = 1;
                    MaxMeleeDamage = 1;
                    break;
            }
            //Appearance:
            switch (IsReloadable)
            {
                case false:
                    Appearance = AllChars.meleeWeaponChar;
                    Color = ConsoleColor.White;
                    Range = 1;
                    break;
                case true:
                    Appearance = AllChars.firearmChar;
                    Color = ConsoleColor.DarkCyan;
                    break;
            }
            CurrentAmmo = MaxAmmo;
        }

    }
}
