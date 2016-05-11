using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class _DEBUG
    {

        public static void AnyShitBeforeStart()
        { //just for testing of methods.
            //randomtest();
            //Console.ReadKey(true);
        }

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

            newwpn = new Weapon("katar");
            newwpn.CoordX = x;
            newwpn.CoordY = y;
            World.AllItemsOnFloor.Add(newwpn);

            Log.AddDebugMessage("items created");
        }

        public static void F6()
        {
            World.player.Hitpoints += 100;
            World.player.Timing.AddActionTime(1000);
            Log.AddDebugMessage("Veeeeery long waiting");
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

            if (keyPressed.Key == ConsoleKey.F6) //development purposes
            {
                F6();
                return true;
            }

            return false;
        }

        static void randomtest()
        {
            const int testmax = 6;
            const long picks = 1000000;
            Algorithms.setSeed();

            var pick = new long[testmax];
            long picksum = 0;

            for (long i = 0; i < testmax; i++)
                pick[i] = 0;

            for (long i = 0; i < picks; i++)
            {
                long j = Algorithms.getRandomInt(testmax);
                picksum += j;
                pick[j]++;
            }

            for (int i = 0; i < testmax; i++)
            {
                Console.Write(i.ToString() + ":x" + pick[i].ToString() + "; ");
                if (i % 9 == 0 && i > 0) Console.WriteLine();
            }

            Console.Write("Pick medium: " + (picksum / picks).ToString());

        }

    }
}
