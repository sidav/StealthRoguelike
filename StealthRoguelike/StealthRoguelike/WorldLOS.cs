using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class WorldLOS //Two-stage LOS check.
    {
        const int mapWidth = Program.mapWidth, mapHeight = Program.mapHeight;
        static int sourceX = -1, sourceY = -1;
        static int lastTimeCheck = -1;
        static bool[,] VisibilityTable = new bool[mapWidth, mapHeight];

        public static bool StraightLOSCheck(int fromx, int fromy, int tox, int toy)
        {   //Checks visible Line of Sight between two tiles
            //Uses straight Brasanham's Line
            //Kinda "first stage check", CAN NOT provide any final result 
            Line.Init(fromx, fromy, tox, toy);
            while (!Line.Step())
            {
                int curX = Line.CurX;
                int curY = Line.CurY;
                if (curX >= mapWidth || curY >= mapHeight || curX < 0 || curY < 0)
                    return false;

                if (World.map[curX, curY].isVisionBlocking)
                {
                    return false;
                }

            }
            return true;
        }

        static bool checkNeighbouringTiles(int x, int y, bool[,] FST)
        {
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    if (x+i < 0 || x+i >= mapWidth || y+j < 0 || y+j >= mapHeight)
                        continue;
                    if (Math.Abs(i * j) == 1)
                        continue;
                    if (!(World.map[x + i, y + j].isVisionBlocking) && FST[x + i, y + j])
                        return true;
                }
            return false;
        }

        static void RecalculateVisibilityTable()
        {
            //First stage
            bool[,] firstStageTable = new bool[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    firstStageTable[i, j] = StraightLOSCheck(sourceX, sourceY, i, j);
            //Second stage
            bool[,] SecondStageTable = new bool[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    if (!firstStageTable[i, j] && World.map[i, j].isVisionBlocking)
                        SecondStageTable[i, j] = checkNeighbouringTiles(i, j, firstStageTable);
                    else SecondStageTable[i, j] = false;
            //Merging stages
            for (int i = 0; i < mapWidth; i++)
                for (int j = 0; j < mapHeight; j++)
                    VisibilityTable[i, j] = firstStageTable[i, j] || SecondStageTable[i, j];
        }

        public static bool VisibleLineExist(int fromx, int fromy, int tox, int toy)
        {
            if (sourceX != fromx || sourceY != fromy || lastTimeCheck != TurnTiming.CurrentTurn)
            {
                sourceX = fromx;
                sourceY = fromy;
                lastTimeCheck = TurnTiming.CurrentTurn;
                RecalculateVisibilityTable();
            }
            return VisibilityTable[tox, toy];
        }

    }
}
