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
            Log.AddLine("Map generation... ok");
            //find an entrance and place player
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    if (map[i, j].IsUpstair)
                        player = UnitCreator.createPlayer(i, j);
            //place enemies
            placeActors();
            Log.AddLine("Actors placement... ok");
            Log.AddLine("All systems nominal.");
        }

        static void makeMap() //generate int-based map 
        {                        // and transform it into tiles array
            //generate map
            int[,] codemap = MapGenerator.generateDungeon();
            //transform int-based map into tiles array
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    map[i, j] = new Tile(codemap[i, j]);
        }

        static void placeActors()
        {
            int plx, ply;
            plx = player.CoordX;
            ply = player.CoordY;
            int x, y;
            for (int i = 0; i < 10; i++)
                do
                {
                    x = Algorithms.getRandomInt(mapWidth);
                    y = Algorithms.getRandomInt(mapHeight);
                    if (map[x, y].IsPassable && !VisibleLineExist(x,y,plx,ply))
                        AllActors.Add(UnitCreator.createActor("Guard", x, y));
                } while (!map[x, y].IsPassable);
        }

        public static bool isItemPresent(int x, int y)
        {
            foreach (Item currItem in AllItemsOnFloor)
                if (currItem.CoordX == x && currItem.CoordY == y)
                    return true;
            return false;
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

        public static bool IsDoor(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            return map[x, y].IsDoor;
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

        public static bool VisibleLineExist(int fromx, int fromy, int tox, int toy)
        {   //Checks visible Line of Sight between two tiles
            Line.Init(fromx, fromy, tox, toy);
            bool prevWasBlocking = false;
            while (!Line.Step())
            {
                int curX = Line.CurX;
                int curY = Line.CurY;
                if (curX >= mapWidth || curY >= mapHeight || curX < 0 || curY < 0)
                    return false;
                if (map[curX, curY].isVisionBlocking)
                {
                    if (Math.Abs(curX - fromx) < 2 && Math.Abs(curY - fromy) < 2)
                    {
                        prevWasBlocking = true;
                        if (!map[curX/* - Line.xmod*/, curY - Line.ymod].isVisionBlocking && Line.deltax > Line.deltay)
                            continue;
                        if (!map[curX - Line.xmod, curY].isVisionBlocking && Line.deltay > Line.deltax)
                            continue;
                    }
                    return false;
                }
                if (prevWasBlocking)
                    return false;
            }

            return true;
        }

        public void Loop()
        {
            ConsoleKeyInfo keyPressed;
            //drawWorld();
            while (true)
            {
                if (player.Timing.IsTimeToAct())
                {
                    WorldRendering.drawWorld(0);
                    player.DrawStatusbar();
                    keyPressed = Console.ReadKey(true);
                    if (keyPressed.Key == ConsoleKey.Escape)
                        break;
                    if (keyPressed.Key == ConsoleKey.F1) //development purposes
                    {
                        WorldRendering.drawWorld(-1);
                        WorldRendering.drawUnits(-1);
                        Console.ReadKey(true);
                        continue;
                    }
                    if (keyPressed.Key == ConsoleKey.F2) //development purposes
                    {
                        for (int j = 0; j < Program.mapHeight; j++)
                            for (int i = 0; i < Program.mapWidth; i++)
                            {
                                map[i, j].WasSeen = true;
                            }
                        continue;
                    }
                    player.handleKeys(keyPressed);
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
                if (player.Hitpoints <= 0)
                {
                    Gameover.ShowGameoverScreen();
                    do
                        keyPressed = Console.ReadKey(true);
                    while (keyPressed.Key != ConsoleKey.Spacebar && keyPressed.Key != ConsoleKey.Escape);
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