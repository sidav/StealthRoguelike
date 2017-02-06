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
        public int CoordX, CoordY; //global coords
        public int lookX = 0, lookY = -1;   //heading vector
        public int ViewAngle = 120; //in degrees.
        public int visibilityRadius = 8; //basic visibility distance
        public char appearance;
        public bool hasFOV;
        public ConsoleColor color;
        public Inventory Inv;
        //public Weapon Wielded;
        public SneakRoleplayStats Stats = new SneakRoleplayStats();
        public int KnockedOutTime = 0; //amount of sleepy turns for knocked out unit

        public Unit(string name, int x, int y, char appear, bool fov, ConsoleColor thiscolor)
        {
            Name = name;
            Hitpoints = GetMaxHitpoints();
            CoordX = x;
            CoordY = y;
            appearance = appear;
            hasFOV = fov;
            color = thiscolor;
            Inv = new Inventory(this);
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

        public int GetMaxHitpoints()
        {
            return (Stats.Level - 1) + (Stats.Endurance / 2);
        }

        protected void drawLookingThingy() //draw a thingy that shows this unit's direction
        {
            if (!World.isActorPresent(CoordX + lookX, CoordY + lookY))
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
            }
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

        public void DrawHighlighted()
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = color;
            Console.SetCursorPosition(this.CoordX, this.CoordY);
            Console.Write(appearance);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = temp;
            //if (hasFOV)
            //    drawLookingThingy();

            //Experimental:
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.SetCursorPosition(this.CoordX, this.CoordY-1);
            //Console.Write("-");
            //Console.SetCursorPosition(this.CoordX - 1, this.CoordY);
            //Console.Write("|");
            //Console.SetCursorPosition(this.CoordX + 1, this.CoordY);
            //Console.Write("|");
            //Console.SetCursorPosition(this.CoordX, this.CoordY + 1);
            //Console.Write("-");
        }

        protected void moveForwardOrOpen()
        {
            if (World.IsDoorPresent(CoordX + lookX, CoordY + lookY))
            {
                if (World.TryUnlockDoor(CoordX + lookX, CoordY + lookY, Inv.GetAllKeys))
                    Timing.AddActionTime(TimeCost.OpenDoorCost(this));
            }
            else 
                TryMoveForward();
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
