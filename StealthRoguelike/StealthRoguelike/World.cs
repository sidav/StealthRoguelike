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
        static Unit Player;
        public const char wallChar = '#';
        public const char closedDoorChar = '+';
        //public const char openedDoorChar = '''';
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
                        Player = new Unit(i, j, '@', true);

        }

        public void drawWorld() //VERY SHITTY AND VERY TEMPORARY SOLUTION. NEED TO REWRITE
        {
            //buffer to draw
            char[,] buffer = new char[Program.consoleWidth, Program.consoleHeight];
            for (int i = 0; i < Program.consoleWidth; i++)
                for (int j = 0; j < Program.consoleHeight; j++)
                    buffer[i, j] = ' ';
            //let's form the buffer
            for (int i = 0; i < MapGenerator.mapWidth; i++)
                for (int j = 0; j < MapGenerator.mapHeight; j++)
                    buffer[i,j] = map[i,j];

            //draw a player
            int x, y;
            x = Player.coordX;
            y = Player.coordY;
            buffer[x, y] = Player.appearance;

            //now let's draw the buffer
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Program.consoleHeight; j++)
                for (int i = 0; i < Program.consoleWidth; i++)
                    if (i > 0 && i < Program.consoleWidth - 1 && j > 0 && j < Program.consoleHeight - 1)
                        if (buffer[i - 1, j - 1] != wallChar || buffer[i, j - 1] != wallChar
                                || buffer[i + 1, j - 1] != wallChar || buffer[i - 1, j] != wallChar ||
                                buffer[i + 1, j] != wallChar || buffer[i - 1, j + 1] != wallChar
                                || buffer[i, j + 1] != wallChar || buffer[i + 1, j + 1] != wallChar)
                            Console.Write(buffer[i, j]);
                        else Console.Write(' ');
                    else Console.Write(wallChar);
        }

    }
}
