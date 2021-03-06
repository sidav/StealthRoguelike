﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    static class WorldRendering
    {


        public static void drawUnits(int mode)
        {
            if (mode == 0) // draw from player
            {
                drawUnitsInCircle(World.player.CoordX, World.player.CoordY, World.player.visibilityRadius);
                World.player.Draw();
            }
            if (mode == -1) //developer mode
            {
                foreach (Actor currActor in World.AllActors)
                {
                    bool t = currActor.hasFOV;
                    currActor.hasFOV = false;
                    currActor.Draw();
                    currActor.hasFOV = t;
                }
                World.player.Draw();
            }
        }

        public static void drawUnitsInCircle(int x, int y, int radius)
        {
            int dx, dy;
            foreach (Actor currActor in World.AllActors)
            {
                dx = currActor.CoordX - x;
                dy = currActor.CoordY - y;
                if (dx * dx + dy * dy > radius * radius)
                    continue;
                if (WorldLOS.VisibleLineExist(x, y, currActor.CoordX, currActor.CoordY))
                    currActor.Draw();
            }
        }

        public static void drawItemsInCircle(int x, int y, int radius)
        {
            int dx, dy;
            foreach (Item currItem in World.AllItemsOnFloor)
            {
                dx = currItem.CoordX - x;
                dy = currItem.CoordY - y;
                if (dx * dx + dy * dy > radius * radius)
                    continue;
                if (WorldLOS.VisibleLineExist(x, y, currItem.CoordX, currItem.CoordY))
                    currItem.Draw();
            }
        }

        public static void drawMap() //TEMPORARY SOLUTION.
        {
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < Program.mapHeight; j++)
                for (int i = 0; i < Program.mapWidth; i++)
                {
                    Console.ForegroundColor = World.map[i, j].Color;
                    Console.Write(World.map[i, j].Appearance);
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
                drawInCircleFOV(World.player.CoordX, World.player.CoordY, World.player.visibilityRadius);
                drawItemsInCircle(World.player.CoordX, World.player.CoordY, World.player.visibilityRadius);
                drawUnits(mode);
            }
            if (mode == -1) //developer mode
            {
                drawMap();
                drawUnits(mode);
            }
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
                    if (xdiff * xdiff + ydiff * ydiff <= radius * radius && WorldLOS.VisibleLineExist(centerx, centery, i, j))
                    {
                        Console.ForegroundColor = World.map[i, j].Color;
                        Console.Write(World.map[i, j].Appearance);
                        World.map[i, j].WasSeen = true;
                        continue;
                    }
                    else
                    {
                        if (World.map[i, j].WasSeen)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(World.map[i, j].Appearance);
                            continue;
                        }
                        if (!World.map[i, j].WasSeen)
                        {
                            Console.Write(' ');
                            continue;
                        }
                    }
                }
        }

        public static void DrawPlayerFOV()
        {
            drawInCircleFOV(World.player.CoordX, World.player.CoordY, World.player.visibilityRadius);
            drawUnitsInCircle(World.player.CoordX, World.player.CoordY, World.player.visibilityRadius);
        }

        public static void DrawWorldAtCoordinate(int x, int y)
        {
            if (World.player.CoordX == x && World.player.CoordY == y)
            {
                World.player.Draw();
                return;
            }
            if (World.isActorPresent(x,y))
            {
                World.getActorAt(x, y).Draw();
                return;
            }
            if (World.isItemPresent(x, y))
            {
                World.getItemAt(x, y).Draw();
                return;
            }
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = World.map[x, y].Color;
            Console.Write(World.map[x, y].Appearance);
        }

        //Separate class maybe?
        public static void DrawLineNotInclusive(int fromx, int fromy, int tox, int toy, char lineChar)
        {
            Line.Init(fromx, fromy, tox, toy);
            while (!Line.Step())
            {
                Console.SetCursorPosition(Line.CurX, Line.CurY);
                Console.Write(lineChar);
            }
            
        }

        public static void DrawTraversingBullet(int fromx, int fromy, int tox, int toy, char bulletChar)
        {
            Line.Init(fromx, fromy, tox, toy);
            int lastx = fromx, lasty = fromy; 
            while (!Line.Step())
            {

                //DrawPlayerFOV();
                //World.player.Draw();
                Console.SetCursorPosition(Line.CurX, Line.CurY);
                Console.Write(bulletChar);
                DrawWorldAtCoordinate(lastx, lasty);
                lastx = Line.CurX;
                lasty = Line.CurY;
                Thread.Sleep(100);
            }

        }


    }
}
