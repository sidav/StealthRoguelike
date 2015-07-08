﻿using System;
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
        const int maxRoomSize = 10;
        const int minCorridorLength = 2;
        const int maxCorridorLength = 5;
        const int maxRooms = 10;
        const int maxCorridors = 15;

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

        static bool isEmpty(int x, int y, int width, int height)
        {
            if (x < 0 || y < 0 || x + width >= mapWidth || y + height >= mapHeight)
                return false;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < width; j++)
                {
                    if (x + i >= mapWidth || y + j >= mapHeight)
                        return false;
                    if (map[x + i, y + j] == '.')
                        return false;
                }
            return true;
        }

        static int corrDirection(int x, int y) //chooses direction for the building corridor
        {
            // 0
            //3#1
            // 2
            int dir = 0;
            if (map[x-1, y] == '.')
                dir = 1;
            if (map[x, y-1] == '.')
                dir = 2;
            if (map[x+1, y] == '.')
                dir = 3;
            return dir;
        }

        static bool digCorridor(int x, int y)
        {
            int corrLength = Tools.getRandomInt(minCorridorLength,maxCorridorLength);
            int dir = corrDirection(x, y);
            //directions:
            // 0
            //3#1
            // 2
            if (dir == 0) //dig up
            {
                if (isEmpty(x - 1, y - corrLength, 3, corrLength))
                    dig(x, y - corrLength, 1, corrLength);
                map[x, y] = '+';
                return true;
            }
            if (dir == 1) //dig right
            {
                if (isEmpty(x, y-1, corrLength, 3))
                    dig(x, y, corrLength, 1);
                map[x, y] = '+';
                return true;
            }
            if (dir == 2) //dig down
            {
                if (isEmpty(x - 1, y, 3, corrLength))
                    dig(x, y, 1, corrLength);
                map[x, y] = '+';
                return true;
            }
            if (dir == 3) //dig left
            {
                if (isEmpty(x-corrLength, y-1, corrLength, 3))
                    dig(x-corrLength, y, corrLength, 1);
                map[x, y] = '+';
                return true;
            }
            map[x, y] = '+';
            return false;
        }

        static bool digRoom(int x, int y)
        {
            int corrLength = Tools.getRandomInt(minCorridorLength, maxCorridorLength);
            int dir = corrDirection(x, y);
            //directions:
            // 0
            //3#1
            // 2
            if (dir == 0) //dig up
            {
                if (isEmpty(x - 1, y - corrLength, 3, corrLength))
                    dig(x, y - corrLength, 1, corrLength);
                map[x, y] = '+';
                return true;
            }
            if (dir == 1) //dig right
            {
                if (isEmpty(x, y - 1, corrLength, 3))
                    dig(x, y, corrLength, 1);
                map[x, y] = '+';
                return true;
            }
            if (dir == 2) //dig down
            {
                if (isEmpty(x - 1, y, 3, corrLength))
                    dig(x, y, 1, corrLength);
                map[x, y] = '+';
                return true;
            }
            if (dir == 3) //dig left
            {
                if (isEmpty(x - corrLength, y - 1, corrLength, 3))
                    dig(x - corrLength, y, corrLength, 1);
                map[x, y] = '+';
                return true;
            }
            map[x, y] = '+';
            return false;
        }

        public static char[,] generateDungeon()
        {
            int roomwidth, roomheight, roomx, roomy;
            //fill everything with "earth"
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    map[i, j] = '#';
            //place a room in the centre
            roomwidth = Tools.getRandomInt(minRoomSize+1, maxRoomSize);
            roomheight = Tools.getRandomInt(minRoomSize+1, maxRoomSize);
            roomx = mapWidth / 2 - roomwidth / 2;
            roomy = mapHeight / 2 - roomheight / 2;
            if (isEmpty(roomx, roomy,roomwidth, roomheight))
                dig(roomx, roomy, roomwidth, roomheight);
            //total rooms and corridors
            int rooms = 1;
            int corridors = 0;
            //now let's start a generation loop
            for (int build = 0; build < maxCorridors + maxRooms; build++)
            {
                //firstly, pick a random wall adjacent to room 
                //or corridor or something
                int pickx = 0, picky = 0;
                while(!isWall(pickx, picky))
                {
                    pickx = Tools.getRandomInt(1, mapWidth - 1);
                    picky = Tools.getRandomInt(1, mapHeight - 1);
                }
                //okay, it's picked. Now let's decide 
                //will we build whether a corridor or a room
                int decision = Tools.getRandomInt(maxRooms + maxCorridors);
                if (decision < maxCorridors) //let's build a corridor
                {
                    if (corridors < maxCorridors)
                    {
                        if (digCorridor(pickx, picky)) corridors++;
                    }
                    else if (rooms < maxRooms)
                        if (digRoom(pickx, picky)) rooms++;
                }
                if (decision >= maxCorridors) //let's build a room
                {
                    if (rooms < maxRooms)
                    {
                        if (digRoom(pickx, picky)) rooms++;
                    }
                    else if (corridors < maxCorridors)
                        if (digCorridor(pickx, picky)) corridors++;
                }
                //повторим...
            }

            return map;
        }
    }
}
