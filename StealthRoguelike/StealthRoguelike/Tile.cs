using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Tile
    {
        public char Appearance;
        public ConsoleColor Color;
        public bool IsPassable, IsDoor, IsOpened, IsLocked, IsUpstair, IsDownstair;

        public Tile(int code)
        {
            Appearance = '&';
            IsPassable = false;
            IsDoor = false;
            Color = ConsoleColor.Magenta;
            IsOpened = false;
            IsLocked = false;
            IsUpstair = false;
            IsDownstair = false;

            CodeToTile(code);
        }

        public void CodeToTile(int code)
        {
            if (code == MapGenerator.wallCode)
            {
                Appearance = World.wallChar;
                Color = ConsoleColor.Gray;
            }
            if (code == MapGenerator.floorCode)
            {
                Appearance = World.floorChar;
                IsPassable = true;
                Color = ConsoleColor.Gray;
            }
            if (code == MapGenerator.doorCode)
            {
                Appearance = World.closedDoorChar;
                IsDoor = true;
                IsOpened = false;
                Color = ConsoleColor.DarkRed;
            }
            if (code == MapGenerator.upstairCode)
            {
                Appearance = World.upstairChar;
                IsUpstair = true;
                Color = ConsoleColor.Gray;
            }
        }

        public bool TryOpenDoor()
        {
            if (IsLocked) return false;
            if (IsDoor && !IsOpened)
            {
                IsOpened = true;
                IsPassable = true;
                Appearance = World.openedDoorChar;
                return true;
            }
            return false;
        }
    }
}
