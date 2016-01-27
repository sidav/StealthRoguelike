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
        public int coordX, coordY; //global coords
        public int lookX = 0, lookY = -1;   //heading vector
        public int ViewAngle = 120; //in degrees.
        public int visibilityRadius = 9; //basic visibility distance
        public char appearance;
        public bool hasFOV;
        public ConsoleColor color;
        public Weapon wielded;

        public Unit(string name, int x, int y, char appear, bool fov, ConsoleColor thiscolor)
        {
            Name = name;
            Hitpoints = 2;
            MaxHitpoints = Hitpoints;
            coordX = x;
            coordY = y;
            appearance = appear;
            hasFOV = fov;
            color = thiscolor;
        }

        public Unit(string name, int x, int y, char appear)
        {
            Name = name;
            coordX = x;
            coordY = y;
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
            Console.SetCursorPosition(coordX + lookX, coordY + lookY);
            Console.Write(thingy);
            //TODO!
        }

        public void Draw()
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(this.coordX, this.coordY);
            Console.Write(appearance);
            if (hasFOV)
                drawLookingThingy();
            Console.ForegroundColor = temp;
        }

        protected void moveForwardOrOpen()
        {
            if (!World.TryOpenDoor(coordX + lookX, coordY + lookY))
                TryMoveForward();
            else
                Timing.AddActionTime(7);
        }

        protected void turnToDirection(int x, int y)
        {
            lookX = x;
            lookY = y;
            Timing.AddActionTime(3);
        }

        public bool TryMoveForward() //move where this unit looks. returns true if the unit has moved.
        {
            if (World.IsPassable(coordX + lookX, coordY + lookY))
            {
                Timing.AddActionTime(10);
                coordX += lookX;
                coordY += lookY;
                return true;
            }
            return false;
        }

    }
}
