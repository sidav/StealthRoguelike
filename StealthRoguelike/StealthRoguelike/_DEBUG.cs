using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class _DEBUG
    {
        public static void InvDbg()
        {
            int x, y;
            //just for lulz
            //x = World.AllActors[0].CoordX;
            //y = World.AllActors[0].CoordY;
            //World.AllActors[0] = UnitCreator.createActor("Kostik",x,y);
            //just for lulz ended
            x = World.player.CoordX;
            y = World.player.CoordY;
            Weapon newwpn = new Weapon("dagger");
            newwpn.CoordX = x;
            newwpn.CoordY = y;
            World.AllItemsOnFloor.Add(newwpn);
            newwpn = new Weapon("baton");
            newwpn.CoordX = x;
            newwpn.CoordY = y;
            World.AllItemsOnFloor.Add(newwpn);
        }
    }
}
