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

        public AmmoTypes type;

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
                    type = AmmoTypes.Arrow;
                    break;
                case "Bullet":
                    type = AmmoTypes.Bullet;
                    break;
                //If you see this ingame, then there is probaby a bug somewhere
                default:
                    name = "DEFAULT_AMMO";
                    type = AmmoTypes.Arrow;
                    break;
            }

        }
    }
}
