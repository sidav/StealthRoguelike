using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class UnitCreator //creates an unit
    {

        public static Player createPlayer(int x, int y)
        {
            Player newPlayer = new Player(x,y);
            newPlayer.Stats.Agility = 12;
            newPlayer.Hitpoints = newPlayer.GetMaxHitpoints();
            Weapon givenWeapon = new Weapon("dagger");
            newPlayer.Inv.Wielded = givenWeapon;
            return newPlayer;
        }

        public static Actor createActor(string name, int x, int y)
        {
            char appearance = '!';
            Weapon givenWeapon;
            switch (name)
            {
                case "Guard":
                    appearance = 'G';
                    givenWeapon = new Weapon("baton");
                    break;
                //just for lulz
                case "Kostik":
                    appearance = 'K';
                    givenWeapon = new Weapon("concept-art drawing");
                    break;
                default:
                    appearance = 'D';
                    givenWeapon = new Weapon("pointless string");
                    break;
            }

            Actor newActor = new Actor(name, x, y, appearance);
            newActor.Inv.Wielded = givenWeapon;
            return newActor;
        }
    }
}
