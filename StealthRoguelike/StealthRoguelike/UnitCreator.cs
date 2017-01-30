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
            char appear = '%';
            Actor newActor = new Actor(name, x, y, appear);
            Weapon givenWeapon;
            switch (name)
            {
                case "Guard":
                    newActor.appearance = 'G';
                    givenWeapon = new Weapon("Baton");
                    break;
                case "Officer":
                    newActor.appearance = 'G';
                    newActor.color = ConsoleColor.Yellow;
                    givenWeapon = new Weapon("Revolver");
                    break;
                //If you see this ingame, then there is probably a bug somewhere
                default:
                    newActor.appearance = '%';
                    givenWeapon = new Weapon("error weapon");
                    break;
            }
            newActor.Inv.Wielded = givenWeapon;
            return newActor;
        }
    }
}
