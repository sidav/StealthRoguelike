using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Ammunition:Item
    {
        public enum AmmoTypes
        {
            Arrow, Bullet, Energycell
        }

        public AmmoTypes TypeOfAmmunition;

        public Ammunition(string ammoName, int qnty):base()
        {
            Appearance = AllChars.AmmunitionChar;
            Color = ConsoleColor.White;

            name = ammoName;
            Quantity = qnty;
            weight = 0;
            switch (name)
            {
                case "Arrow":
                    TypeOfAmmunition = AmmoTypes.Arrow;
                    break;
                case "Bullet":
                    TypeOfAmmunition = AmmoTypes.Bullet;
                    break;
                //If you see this ingame, then there is probably a bug somewhere
                default:
                    name = "??DEFAULT_AMMO??";
                    TypeOfAmmunition = AmmoTypes.Arrow;
                    break;
            }

        }
    }
}
