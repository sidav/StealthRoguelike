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

        public static Actor guard; ///!!!TEMPORARY SOLUTION!!! FOR AI DEVELOPMENT ONLY!
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
                        player = new Player(i, j);
            int x, y;
            do
            {
                x = Algorithms.getRandomInt(mapWidth);
                y = Algorithms.getRandomInt(mapHeight);
                if (IsPassable(x,y))
                    guard = new Actor("Our subject", x, y, 'G');
            } while (!IsPassable(x,y));
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


        public static bool IsPassable(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
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
        {
            Line.Init(fromx, fromy, tox, toy);
            while (!Line.Step())
            {
                int curX = Line.CurX;
                int curY = Line.CurY;
                if (curX >= mapWidth || curY >= mapHeight || curX < 0 || curY < 0)
                    return false;
                if (map[curX, curY].isVisionBlocking)
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
                WorldRendering.drawWorld(0);
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
                player.handleKeys(keyPressed);
                guard.DoSomething();
            }

        }

    }
}
