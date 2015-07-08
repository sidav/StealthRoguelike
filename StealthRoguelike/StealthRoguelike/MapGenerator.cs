using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class MapGenerator
    {
        static int mapWidth, mapHeight;
        static char[,] map;
        const int minRoomSize = 4;
        const int maxRoomSize = 15;
        const int maxRooms = 8;
        const int minCorridors = 4;

        public static void setParams(int mapw, int maph)
        {
            mapWidth = mapw;
            mapHeight = maph;
            map = new char[mapWidth, mapHeight];
        }

        static bool dig(int x, int y, int roomwidth, int roomheight)
        {
            if (x < 0 || y < 0 || x + roomwidth >= mapWidth || y + roomheight >= mapHeight)
                return false;
            for (int i = 0; i < roomwidth; i++)
                for (int j = 0; j < roomheight; j++)
                    map[x+i, y+j] = '.';
            return true; 
        }
       
        static bool isWall(int x, int y)
        {
            if (map[x, y] != '#')
                return false;
            if (x == 0 || x == mapWidth-1 || y == 0 || y == mapHeight-1)
                return false;
            if (map[x-1,y] == '.' || map[x+1,y] == '.' || map[x,y-1] == '.' || map[x,y+1] == '.')
                return true;
            return false;
        }

        public static char[,] generateDungeon()
        {
            int roomwidth, roomheight, roomx, roomy;

            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    map[i, j] = '#';
            //place a room in the centre
            roomwidth = Tools.getRandomInt(minRoomSize, maxRoomSize);
            roomheight = Tools.getRandomInt(minRoomSize, maxRoomSize);
            roomx = mapWidth / 2 - roomwidth / 2;
            roomy = mapHeight / 2 - roomheight / 2;
            dig(roomx, roomy, roomwidth, roomheight);
            //now let's start a generation loop
            for (int roomnum = 0; roomnum < maxRooms; roomnum++)
            {
                //firstly, pick a random wall
                int pickx = 0, picky = 0;
                while(!isWall(pickx, picky))
                {
                    pickx = Tools.getRandomInt(1, mapWidth - 1);
                    picky = Tools.getRandomInt(1, mapHeight - 1);
                }
                //okay, it's picked. Now let's decide 
                //will we build whether a corridor or a room

            }

            return map;
        }
    }
}
