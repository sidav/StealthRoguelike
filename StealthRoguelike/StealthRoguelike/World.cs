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

        public static Pawn guard; ///!!!TEMPORARY SOLUTION!!! FOR AI DEVELOPMENT ONLY!
        static Player player;

        public const int mapWidth = Program.mapWidth;
        public const int mapHeight = Program.mapHeight;

        public World()
        {
            makeMap();
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
                    guard = new Pawn(x, y, 'G');
            } while (!IsPassable(x,y));
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

        public static void drawUnits(int mode)
        {
            if (mode == 0) // draw from player
            {
                if (VisibleLineExist(player.coordX, player.coordY, guard.coordX, guard.coordY))
                    guard.Draw();
                player.Draw();
            }
            if (mode == -1) //developer mode
            {
                guard.Draw();
                player.Draw();
            }
        }

        public static void drawUnitsInCircle(int x, int y, int radius)
        {
            int dx = guard.coordX - x;
            int dy = guard.coordY - y;
            if (dx * dx + dy * dy > radius * radius)
                return;
            if (VisibleLineExist(x, y, guard.coordX, guard.coordY))
                guard.Draw();
        }

        public static void drawMap() //TEMPORARY SOLUTION.
        { 
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Program.mapHeight; j++)
                for (int i = 0; i < Program.mapWidth; i++)
                {
                    Console.ForegroundColor = map[i, j].Color;
                    Console.Write(map[i, j].Appearance);
                }
        }

        //public static void Redraw(int x, int y)
        //{
        //    if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
        //        return;
        //    Console.SetCursorPosition(x, y);
        //    Console.ForegroundColor = map[x, y].Color;
        //    Console.Write(map[x, y].Appearance);
        //    drawUnits();
        //}

        public static void drawWorld(int mode)
        {
            if (mode == 0) //draw from player
            {
                drawInCircleFOV(player.coordX, player.coordY, player.visibilityRadius);
                drawUnits(mode);
            }
            if (mode == -1) //developer mode
            {
                drawMap();
                drawUnits(mode);
            }
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

        public static void drawInCircleFOV(int centerx, int centery, int radius)
        { // will draw in "fair" circle with vision ray tracing
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Program.mapHeight; j++)
                for (int i = 0; i < Program.mapWidth; i++)
                {
                    //Console.SetCursorPosition(i, j);
                    int xdiff = centerx - i;
                    int ydiff = centery - j;
                    if (xdiff * xdiff + ydiff * ydiff <= radius * radius && VisibleLineExist(centerx, centery, i, j))
                    {
                            Console.ForegroundColor = map[i, j].Color;
                            Console.Write(map[i, j].Appearance);
                            map[i, j].WasSeen = true;
                            continue;
                    }
                    else 
                    {
                        if (map[i, j].WasSeen)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(map[i, j].Appearance);
                            continue;
                        }
                        if (!map[i, j].WasSeen)
                        {
                            Console.Write(' ');
                            continue;
                        }
                    }
                }
        }

        public void Loop()
        {
            ConsoleKeyInfo keyPressed;
            //drawWorld();
            while (true)
            {
                drawWorld(0);
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape)
                    break;
                player.handleKeys(keyPressed);
                guard.DoSomething();
            }

        }

    }
}
