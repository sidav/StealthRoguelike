using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Unit
    {
        public TurnTiming Timing = new TurnTiming();
        public string Name;
        public int Hitpoints;
        public int MaxHitpoints;
        public int CoordX, CoordY; //global coords
        public int lookX = 0, lookY = -1;   //heading vector
        public int ViewAngle = 120; //in degrees.
        public int visibilityRadius = 8; //basic visibility distance
        public char appearance;
        public bool hasFOV;
        public ConsoleColor color;
        public Inventory Inv = new Inventory();
        //public Weapon Wielded;
        public int KnockedOutTime = 0; //amount of sleepy turns for knocked out unit

        public Unit(string name, int x, int y, char appear, bool fov, ConsoleColor thiscolor)
        {
            Name = name;
            MaxHitpoints = 2;
            Hitpoints = MaxHitpoints;
            CoordX = x;
            CoordY = y;
            appearance = appear;
            hasFOV = fov;
            color = thiscolor;
        }

        public Unit(string name, int x, int y, char appear)
        {
            Name = name;
            CoordX = x;
            CoordY = y;
            appearance = appear;
            hasFOV = false;
            color = ConsoleColor.White;
        }

        protected void drawLookingThingy() //draw a thingy that shows this unit's direction
        {
            char thingy = '?';
            if (lookX == 0)
                thingy = '|';
            if (lookY == 0)
                thingy = '-';
            if (lookX * lookY == 1)
                thingy = '\\';
            if (lookX * lookY == -1)
                thingy = '/';
            Console.SetCursorPosition(CoordX + lookX, CoordY + lookY);
            Console.Write(thingy);
            //TODO!
        }

        public void Draw()
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(this.CoordX, this.CoordY);
            Console.Write(appearance);
            if (hasFOV)
                drawLookingThingy();
            Console.ForegroundColor = temp;
        }

        protected void moveForwardOrOpen()
        {
            if (!World.TryOpenDoor(CoordX + lookX, CoordY + lookY))
                TryMoveForward();
            else
                Timing.AddActionTime(TimeCost.OpenDoorCost(this));
        }

        protected void turnToDirection(int x, int y)
        {
            lookX = x;
            lookY = y;
            Timing.AddActionTime(TimeCost.TurningCost(this));
        }

        public bool TryMoveForward() //move where this unit looks. returns true if the unit has moved.
        {
            if (World.IsPassable(CoordX + lookX, CoordY + lookY))
            {
                Timing.AddActionTime(TimeCost.MoveCost(this));
                CoordX += lookX;
                CoordY += lookY;
                return true;
            }
            return false;
        }

        public virtual bool IsUnaware()
        {
            return true;
        }

    }
}
