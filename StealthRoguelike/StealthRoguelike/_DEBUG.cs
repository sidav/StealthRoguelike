﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class _DEBUG
    {
        public static void F2()
        {
            WorldRendering.drawWorld(-1);
            WorldRendering.drawUnits(-1);
            Log.AddDebugMessage("clairvoyance... Press any key");
            Console.ReadKey(true);
        }

        public static void F3()
        {
            for (int j = 0; j < Program.mapHeight; j++)
                for (int i = 0; i < Program.mapWidth; i++)
                {
                    if (World.map[i, j].IsPassable)
                        World.map[i, j].WasSeen = true;
                }
            Log.AddDebugMessage("mapping");
        }

        public static void F4()
        {
            World.player.Hitpoints += World.player.MaxHitpoints;
            Log.AddDebugMessage("health added");
        }

        public static void F5()
        {
            int x, y;
            //just for lulz
            //x = World.AllActors[0].CoordX;
            //y = World.AllActors[0].CoordY;
            //World.AllActors[0] = UnitCreator.createActor("Kostik", x, y);
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
            Log.AddDebugMessage("items created");
        }


        public static bool DebugKey(ConsoleKeyInfo keyPressed)
        {
            if (keyPressed.Key == ConsoleKey.F2) //development purposes
            {
                F2();
                return true;
            }
            if (keyPressed.Key == ConsoleKey.F3) //development purposes
            {
                F3();
                return true;
            }
            if (keyPressed.Key == ConsoleKey.F4) //development purposes
            {
                F4();
                return true;
            }
            if (keyPressed.Key == ConsoleKey.F5) //development purposes
            {
                F5();
                return true;
            }
            return false;
        }

    }
}
