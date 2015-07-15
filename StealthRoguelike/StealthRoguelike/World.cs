using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class World
    {
        public static char[,] map;
        static Player player;

        public const int mapWidth = Program.mapWidth;
        public const int mapHeight = Program.mapHeight;

        public const char wallChar = '#';
        public const char closedDoorChar = '+';
        public const char openedDoorChar = '\'';
        public const char floorChar = '.';
        public const char upstairChar = '<';
        public const char downstairChar = '>';

        public World()
        {
            //generate map
            map = MapGenerator.generateDungeon();
            //find an entrance 
            for (int i = 0; i < MapGenerator.mapWidth; i++)
                for (int j = 0; j < MapGenerator.mapHeight; j++)
                    if (map[i, j] == upstairChar)
                        player = new Player(i,j);

        }

        public static bool isWalkable(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            if (map[x, y] == wallChar)
                return false;
            if (map[x, y] == closedDoorChar)
                return false;
            return true;
        }

        public static bool tryOpenDoor(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return false;
            if (map[x, y] == closedDoorChar)
            {
                map[x, y] = openedDoorChar;
                return true;
            }
            return false;
        }

        public static void drawUnits()
        {
            player.Draw();
        }

        public void drawMap() //TEMPORARY SOLUTION.
        { 
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Program.mapHeight; j++)
                for (int i = 0; i < Program.mapWidth; i++)
                    //if (i != Program.mapWidth - 1 || j != Program.mapHeight - 1)
                        Console.Write(map[i, j]);
        }

        public static void Redraw(int x, int y)
        {
            if (x < 0 || x >= mapWidth || y < 0 || y >= mapHeight)
                return;
            Console.SetCursorPosition(x, y);
            Console.Write(map[x, y]);
            drawUnits();
        }

        public void drawWorld()
        {
            drawMap();
            drawUnits();
        }

        public void Loop()
        {
            ConsoleKeyInfo keyPressed;
            drawWorld();
            while (true)
            {
                keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape)
                    break;
                player.handleKeys(keyPressed);
                drawUnits();
            }

        }

    }
}
