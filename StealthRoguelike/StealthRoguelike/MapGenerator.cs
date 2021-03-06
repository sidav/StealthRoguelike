﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class MapGenerator
    {

        //IMPORTANT(!!!) CONSTANTS
        const int minRoomSize = 2;
        const int maxRoomSize = 12;
        const int minCorridorLength = 3;
        const int maxCorridorLength = 15;
        const int maxRooms = 35;//35;
        const int maxCorridors = 15;//25;
        const int maxColumns = 25;
        const int maxTries = 1000; //maximum amount of tries for structure placing

        public enum piece {wall, floor, door, upstair, downstair, keyplace};
        public static int mapWidth, mapHeight;
        static piece[,] map;
        public static int[,] lockMap; //for the locked doors placement
        static int maxKeys = 0; //max amount of different keys needed for the map
        static int currentLockLevel = 0;
        public const int wallCode = (int)piece.wall;
        public const int floorCode = (int)piece.floor;
        public const int doorCode = (int)piece.door;
        public const int upstairCode = (int)piece.upstair;
        public const int downstairCode = (int)piece.downstair;
        public const int keyplaceCode = (int)piece.keyplace;
        public const char wallChar = '#';
        public const char doorChar = '+';
        public const char floorChar = '.';
        public const char upstairChar = '<';
        public const char downstairChar = '>';

        public static void setParams(int mapw, int maph, int mk)
        {
            mapWidth = mapw;
            mapHeight = maph;
            map = new piece[mapWidth, mapHeight];
            lockMap = new int[mapWidth, mapHeight];
            maxKeys = mk;
        }

        static bool dig(int x, int y, int roomwidth, int roomheight)
        {
            if (x < 0 || y < 0 || x + roomwidth >= mapWidth || y + roomheight >= mapHeight)
                return false;
            for (int i = 0; i < roomwidth; i++)
                for (int j = 0; j < roomheight; j++)
                {
                    map[x + i, y + j] = piece.floor;
                    lockMap[x + i, y + j] = currentLockLevel;
                }
            return true; 
        }
       
        static bool isWall(int x, int y)
        {
            if (map[x, y] != piece.wall)
                return false;
            if (x == 0 || x == mapWidth-1 || y == 0 || y == mapHeight-1)
                return false;
            if (map[x-1,y] == piece.floor || map[x+1,y] == piece.floor || map[x,y-1] == piece.floor || map[x,y+1] == piece.floor)
                return true;
            return false;
        }

        static bool isEmpty(int x, int y, int width, int height)
        {
            if (x < 0 || y < 0 || x + width >= mapWidth || y + height >= mapHeight)
                return false;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (x + i >= mapWidth || y + j >= mapHeight)
                        return false;
                    if (map[x + i, y + j] != piece.wall/*== piece.floor || map[x + i, y + j] == piece.door*/)
                        return false;
                }
            return true;
        }

        static int corrDirection(int x, int y) //chooses direction for the building corridor
        {
            // 0
            //3#1
            // 2
            int dir = -1;
            if (map[x, y+1] == piece.floor)
                dir = 0;
            if (map[x-1, y] == piece.floor)
                dir = 1;
            if (map[x, y-1] == piece.floor)
                dir = 2;
            if (map[x+1, y] == piece.floor)
                dir = 3;
            return dir;
        }

        static bool digCorridor(int x, int y) // HERE BE DRAGONS
        {
            int corrLength = MyRandom.getRandomInt(minCorridorLength,maxCorridorLength);
            int dir = corrDirection(x, y);
            //directions:
            // 0
            //3#1
            // 2
            if (dir == 0) //dig up
            {
                if (isEmpty(x - 1, y - corrLength, 3, corrLength+1))
                {
                    dig(x, y - corrLength, 1, corrLength);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 1) //dig right
            {
                if (isEmpty(x, y - 1, corrLength+1, 3))
                {
                    dig(x, y, corrLength, 1);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 2) //dig down
            {
                if (isEmpty(x - 1, y, 3, corrLength+1))
                {
                    dig(x, y, 1, corrLength);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 3) //dig left
            {
                if (isEmpty(x - corrLength, y - 1, corrLength+1, 3))
                {
                    dig(x - corrLength, y, corrLength, 1);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            return false;
        }

        static bool digRoom(int x, int y) // HERE BE DRAGONS
        {
            int roomWidth = MyRandom.getRandomInt(minRoomSize, maxRoomSize);
            int roomHeight = MyRandom.getRandomInt(minRoomSize, maxRoomSize);
            int dir = corrDirection(x, y);

            ////DEBUG
            //if (dir != 1) return false;
            ////DEBUG ENDED

            //directions:
            // 0
            //3#1
            // 2
            if (dir == 0) //dig up
            {
                int intersect = MyRandom.getRandomInt(roomWidth);
                if (isEmpty(x - intersect - 2, y - roomHeight-1, roomWidth + 2, roomHeight + 2))
                {
                    dig(x - intersect, y - roomHeight, roomWidth/* + 1*/, roomHeight);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 1) //dig right
            {
                int intersect = MyRandom.getRandomInt(roomHeight);
                if (isEmpty(x, y - intersect - 2, roomWidth + 2, roomHeight + 2))
                {
                    dig(x + 1, y - intersect, roomWidth, roomHeight);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 2) //dig down
            {
                int intersect = MyRandom.getRandomInt(roomWidth);
                if (isEmpty(x - intersect - 2, y, roomWidth + 2, roomHeight + 2))
                {
                    dig(x - intersect, y+1, roomWidth, roomHeight);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            if (dir == 3) //dig left
            {
                int intersect = MyRandom.getRandomInt(roomHeight);
                if (isEmpty(x - roomWidth - 1 , y - intersect - 2, roomWidth + 2, roomHeight + 2))
                {
                    dig(x - roomWidth, y - intersect, roomWidth, roomHeight);
                    map[x, y] = piece.door;
                    lockMap[x, y] = currentLockLevel;
                    return true;
                }
            }
            return false;
        }

        static void tryAddCorridor() //this tries to build a random corridor
        {                            //in the dungeon      
            int x, y;
            bool done = false;
            for (int i = 0; i < maxTries; i++)
            {
                x = 0; y = 0;
                while (!isWall(x, y))
                {
                    x = MyRandom.getRandomInt(1, mapWidth - 1);
                    y = MyRandom.getRandomInt(1, mapHeight - 1);
                }
                done = digCorridor(x, y);
                if (done) break;
            }
        }

        static void tryAddRoom() //this tries to build a random room
        {
            int x, y;
            bool done = false;
            for (int i = 0; i < maxTries; i++)
            {
                x = 0; y = 0;
                while (!isWall(x, y))
                {
                    x = MyRandom.getRandomInt(1, mapWidth - 1);
                    y = MyRandom.getRandomInt(1, mapHeight - 1);
                }
                done = digRoom(x, y);
                if (done) break;
            }
        }

        static void tryAddKeyplace() //this tries to add a tile where the key should be placed.
        {
            int x = MyRandom.getRandomInt(1, mapWidth - 1);
            int y = MyRandom.getRandomInt(1, mapHeight - 1);
            //for (int i = 0; i < maxTries; i++)
            //{
            while (map[x,y] != piece.floor || lockMap[x,y] != currentLockLevel)
                {
                    x = MyRandom.getRandomInt(1, mapWidth - 1);
                    y = MyRandom.getRandomInt(1, mapHeight - 1);
                }
                map[x, y] = piece.keyplace;
                lockMap[x, y] = currentLockLevel + 1;
            //    break;
            //}
        }

        static void addColumns()
        {
            for (int i = 0; i < maxColumns; i++)
                for (int j = 0; j < maxTries; j++)
                {
                    bool wrongCoords = false;
                    int x = MyRandom.getRandomInt(1, mapWidth-1);
                    int y = MyRandom.getRandomInt(1, mapHeight-1);
                    for (int xx = -1; xx < 2; xx++)
                        for (int yy = -1; yy < 2; yy++)
                            if (map[x + xx, y + yy] != piece.floor)
                                wrongCoords = true;
                    if (wrongCoords == true) continue;
                    map[x, y] = piece.wall;
                    break;
                }
        }

        public static int[,] generateDungeon()
        {
            int roomwidth, roomheight, roomx, roomy;
            //fill everything with "earth"
            //set everything with zero lock level as well
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j] = piece.wall;
                    lockMap[i, j] = currentLockLevel;
                }
            //place a room in the center of a dungeon
            roomwidth = MyRandom.getRandomInt(minRoomSize+1, maxRoomSize);
            roomheight = MyRandom.getRandomInt(minRoomSize+1, maxRoomSize);
            roomx = mapWidth / 2 - roomwidth / 2;
            roomy = mapHeight / 2 - roomheight / 2;
            if (isEmpty(roomx, roomy,roomwidth, roomheight))
                dig(roomx, roomy, roomwidth, roomheight);
            //total rooms and corridors
            int rooms = 1;
            int corridors = 0;
            //now let's start a generation loop
            int iteration = 0;
            while (corridors < maxCorridors || rooms < maxRooms)
            {
                //do we need to increase the current lock level?
                //TEMPORARY SOLUTION!!111
                //Hehehe... Temporary...
                for (int i = 0; i <= maxKeys; i++)
                {
                    if (rooms > i * maxRooms / (maxKeys + 1) && i > currentLockLevel)
                    {
                        tryAddKeyplace();
                        currentLockLevel = i;
                    }
                }

                //firstly, pick a random wall adjacent to room 
                //or corridor or something
                    ///!!MOVED TO ANOTHER METHOD!!
                //okay, it's picked. Now let's decide 
                //will we build whether a corridor or a room
                if (true)
                {
                    if (corridors < maxCorridors)
                    {
                        tryAddCorridor();
                        corridors++;
                    }
                }
                if (true /*iteration % 2 == 0*/)
                {
                    if (rooms < maxRooms)
                    {
                        tryAddRoom();
                        rooms++;
                    }
                }
                //repeat...
                iteration++;
            }
            //now let's make walls on perimeter
            for (int i = 0; i < mapWidth; i++)
            {
                map[i, 0] = piece.wall;
                map[i, mapHeight-1] = piece.wall;
            }
            for (int j = 0; j < mapHeight; j++)
                {
                    map[0, j] = piece.wall;
                    map[mapWidth-1, j] = piece.wall;
                }

            //let's place an entrance stair
            int sx = 0, sy = 0;
            while (map[sx, sy] != piece.floor || lockMap[sx, sy] != 0)
            {
                sx = MyRandom.getRandomInt(mapWidth);
                sy = MyRandom.getRandomInt(mapHeight);
            }
            map[sx, sy] = piece.upstair;

            //let's add some columns
            addColumns();

            //transform "pieces" into ints
            int[,] finalMap = new int[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    finalMap[i, j] = (int)map[i, j];
            
            return finalMap;
        }

    }
}
