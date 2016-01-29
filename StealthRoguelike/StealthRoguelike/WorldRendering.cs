﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    static class WorldRendering
    {


        public static void drawUnits(int mode)
        {
            if (mode == 0) // draw from player
            {
                drawUnitsInCircle(World.player.coordX, World.player.coordY, World.player.visibilityRadius);
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
            int dx, dy; /*actorx, actory;*/
            foreach (Actor currActor in World.AllActors)
            {
                //actorx = currActor.coordX;
                //actory = currActor.coordY;
                dx = currActor.coordX - x;
                dy = currActor.coordY - y;
                //Log.AddLine("DEBUG x=" + x.ToString() + " y=" + y.ToString() + " dx = " + dx.ToString() + " dy = " + dy.ToString());
                //Log.AddLine("DEBUG ACTx=" + actorx.ToString() + " ACTy=" + actory.ToString());
                if (dx * dx + dy * dy > radius * radius)
                    continue;
                if (World.VisibleLineExist(x, y, currActor.coordX, currActor.coordY))
                    currActor.Draw();
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
                drawInCircleFOV(World.player.coordX, World.player.coordY, World.player.visibilityRadius);
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
                    if (xdiff * xdiff + ydiff * ydiff <= radius * radius && World.VisibleLineExist(centerx, centery, i, j))
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


    }
}
