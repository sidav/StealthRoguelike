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
        public bool WasSeen;
        public bool IsPassable, isVisionBlocking, IsDoor, IsOpened, IsLocked, IsUpstair, IsDownstair;

        public Tile(int code)
        {
            WasSeen = false;
            Appearance = '&';
            IsPassable = false;
            IsDoor = false;
            Color = ConsoleColor.Magenta;
            IsOpened = false;
            IsLocked = false;
            IsUpstair = false;
            IsDownstair = false;
            isVisionBlocking = true;
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
                isVisionBlocking = false;
            }
            if (code == MapGenerator.doorCode)
            {
                Appearance = World.closedDoorChar;
                IsDoor = true;
                IsOpened = false;
                Color = ConsoleColor.DarkMagenta;
            }
            if (code == MapGenerator.upstairCode)
            {
                IsPassable = true;
                Appearance = World.upstairChar;
                IsUpstair = true;
                isVisionBlocking = false;
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
                isVisionBlocking = false;
                Appearance = World.openedDoorChar;
                return true;
            }
            return false;
        }
    }
}
