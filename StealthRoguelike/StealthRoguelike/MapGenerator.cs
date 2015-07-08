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
        const int minRoomSize = 2;
        const int maxRoomSize = 15;
        const int maxRooms = 8;
        const int minCorridors = 4;

        public static void setParams(int mapw, int maph)
        {
            mapWidth = mapw;
            mapHeight = maph;
            map = new char[mapWidth, mapHeight];
        }

        static bool digRoom(int x, int y, int roomwidth, int roomheight)
        {
            if (x < 0 || y < 0 || x + roomwidth > mapWidth || y + roomheight > mapHeight)
                return false;
            for (int i = 0; i < roomwidth; i++)
                for (int j = 0; j < roomheight; j++)
                    map[x+i, y+j] = '.';
            return true; 
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
            digRoom(roomx, roomy, roomwidth, roomheight);
            

            //for (int roomnum = 0; roomnum < maxRooms; roomnum++)
            //{
            //    bool success = false;
            //    while (!success)
            //    {
            //        roomwidth = Tools.getRandomInt(minRoomSize, maxRoomSize);
            //        roomheight = Tools.getRandomInt(minRoomSize, maxRoomSize);
            //        roomx = Tools.getRandomInt(mapWidth);
            //        roomy = Tools.getRandomInt(mapHeight);
            //        success = digRoom(roomx, roomy, roomwidth, roomheight);
            //    }
            //}

            return map;
        }
    }
}
