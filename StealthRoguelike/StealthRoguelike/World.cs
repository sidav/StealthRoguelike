using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class World
    {
        public static Tile[,] map = new Tile[mapWidth, mapHeight];

        public static List<Actor> AllActors = new List<Actor>(); ///!!!STILL TEMPORARY SOLUTION!!!
        public static List<Item> AllItemsOnFloor = new List<Item>();
        public static Player player;

        public const int mapWidth = Program.mapWidth;
        public const int mapHeight = Program.mapHeight;

        public World()
        {
            makeMap();
            _DEBUG.AddDebugMessage("Map generation... ok");
            //find an entrance and place player
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    if (map[i, j].IsUpstair)
                        player = UnitCreator.createPlayer(i, j);
            //place enemies
            placeActors();
            _DEBUG.AddDebugMessage("Actors placement... ok");
            _DEBUG.AddDebugMessage("All systems nominal... for now");
            _DEBUG.AddDebugMessage("Seed for this world is " + MyRandom.Seed.ToString());
            Log.AddLine("Press F1 for list of game commands");
        }

        static void makeMap() //generate int-based map 
        {                        // and transform it into tiles array
            //generate map
            int[,] codemap = MapGenerator.generateDungeon();
            int[,] lockmap = MapGenerator.lockMap;
            //transform int-based map into tiles array
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j] = new Tile(codemap[i, j], lockmap[i, j]);

                }
            //Replace keyplaces with floor tiles with the key...
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                {
                    if (map[i,j].isKeyPlace)
                    {
                        int keylvl = map[i, j].lockLevel;
                        //map[i, j] = new Tile(MapGenerator.floorCode, keylvl);
                        AllItemsOnFloor.Add(new Key(i, j, keylvl));
                    }

                }
        }

        static void placeActors()
        {
            int plx, ply;
            plx = player.CoordX;
            ply = player.CoordY;
            int x, y;
            for (int i = 0; i < 7; i++)
                do
                {
                    x = MyRandom.getRandomInt(mapWidth);
                    y = MyRandom.getRandomInt(mapHeight);
                    if (map[x, y].IsPassable && !WorldLOS.VisibleLineExist(x,y,plx,ply))
                        AllActors.Add(UnitCreator.createActor("Guard", x, y));
                } while (!map[x, y].IsPassable);
            for (int i = 0; i < 3; i++)
                do
                {
                    x = MyRandom.getRandomInt(mapWidth);
                    y = MyRandom.getRandomInt(mapHeight);
                    if (map[x, y].IsPassable && !WorldLOS.VisibleLineExist(x, y, plx, ply))
                        AllActors.Add(UnitCreator.createActor("Officer", x, y));
                } while (!map[x, y].IsPassable);
        }

        public static bool isItemPresent(int x, int y)
        {
            foreach (Item currItem in AllItemsOnFloor)
                if (currItem.CoordX == x && currItem.CoordY == y)
                    return true;
            return false;
        }

        public static List<Item> getItemListAt(int x, int y)
        {
            List<Item> list = new List<Item>();
            foreach (Item currItem in AllItemsOnFloor)
                if (currItem.CoordX == x && currItem.CoordY == y)
                    list.Add(currItem);
            return list;
        }

        public static Item getItemAt(int x, int y)
        {
            foreach (Item currItem in AllItemsOnFloor)
                if (currItem.CoordX == x && currItem.CoordY == y)
                    return currItem;
            return null;
        }

        public static bool isActorPresent(int x, int y)
        {
            //if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
            //    return false;
            foreach (Actor currActor in AllActors)
                if (currActor.CoordX == x && currActor.CoordY == y)
                    return true;
            return false;
        }

        public static Actor getActorAt(int x, int y)
        {
            foreach (Actor currActor in AllActors)
                if (currActor.CoordX == x && currActor.CoordY == y)
                    return currActor;
            return null;
        }

        public static bool IsPassable(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            if (player.CoordX == x && player.CoordY == y)
                return false;
            if (isActorPresent(x, y))
                return false;
            return map[x,y].IsPassable;
        }

        public static bool IsDoorPresent(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            return map[x, y].IsDoor;
        }

        public static bool TryUnlockDoor(int x, int y, List<Key> keys)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            return map[x, y].TryUnlockDoor(keys);
        }

        public static bool TryOpenDoor(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            return map[x,y].TryOpenDoor();
        }

        public static bool TryCloseDoor(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            return map[x, y].TryCloseDoor();
        }

        public void Loop()
        {
            ConsoleKeyInfo keyPressed;
            //drawWorld();
            while (true)
            {
                if (player.Timing.IsTimeToAct())
                {
                    if (player.seesAsUsual())
                        WorldRendering.drawWorld(0);
                    player.DrawStatusbar();
                    keyPressed = Console.ReadKey(true);
                    if (keyPressed.Key == ConsoleKey.Escape)
                        break;
                    if (keyPressed.Key == ConsoleKey.F1)
                    {
                        HelpScreen.DrawHelpScreen();
                        Log.DrawMiniLog();
                        continue;
                    }

                    if (_DEBUG.DebugKey(keyPressed)) //DEBUG-ONLY
                        continue;

                    player.handleKeys(keyPressed);

                    if (player.Timing.IsTimeToAct()) //don't do anything if player did nothing at all
                        continue;
                }
                for (int i = 0; i < AllActors.Count; i++)
                {
                    Actor currActor = AllActors[i];
                    if (currActor.Hitpoints <= 0)
                    {
                        AllItemsOnFloor.Add(new Corpse(currActor));
                        Log.AddLine(currActor.Name + " drops dead!");
                        AllActors.Remove(currActor);
                        i--;
                        continue;
                    }
                    if (currActor.KnockedOutTime > 0)
                    {
                        AllItemsOnFloor.Add(new UnconsciousBody(currActor));
                        Log.AddLine(currActor.Name + " falls unconscious!");
                        AllActors.Remove(currActor);
                        i--;
                        continue;
                    }
                    if (currActor.Timing.IsTimeToAct())
                        currActor.DoSomething();
                }
                if (player.Hitpoints <= 0) //GAME OVER
                {
                    Gameover.ShowGameoverScreen();
                    break;
                }
                for (int i = 0; i < AllItemsOnFloor.Count; i++)
                {
                    if (AllItemsOnFloor[i] is UnconsciousBody)
                    {
                        UnconsciousBody curr = ((UnconsciousBody)AllItemsOnFloor[i]);
                        if (curr.TimeToWakeUp())
                        {
                            AllActors.Add((Actor)curr.Knocked);
                            Log.AddLine(((Actor)curr.Knocked).Name + " wakes up!");
                            AllItemsOnFloor.Remove(curr);
                            i--;
                            continue;
                        }
                    }
                }
                TurnTiming.Tick();
            }

        }

    }
}