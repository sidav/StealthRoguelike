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
        public int lockLevel = 0;

        public Tile(int code, int locklvl)
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
            CodeToTile(code, locklvl);
        }

        public void CodeToTile(int code, int locklvl)
        {
            if (code == MapGenerator.wallCode)
            {
                Appearance = AllChars.wallChar;
                Color = ConsoleColor.Gray;
            }
            if (code == MapGenerator.floorCode)
            {
                Appearance = AllChars.floorChar;
                IsPassable = true;
                Color = ConsoleColor.Gray;
                isVisionBlocking = false;
            }
            if (code == MapGenerator.doorCode)
            {
                Appearance = AllChars.closedDoorChar;
                IsDoor = true;
                IsOpened = false;
                switch (locklvl)
                {
                    case 0: Color = ConsoleColor.DarkMagenta; break;
                    case 1: Color = ConsoleColor.Green; break;
                    case 2: Color = ConsoleColor.DarkYellow; break;
                    default: Color = ConsoleColor.Green; break;
                }
                lockLevel = locklvl;
            }
            if (code == MapGenerator.upstairCode)
            {
                IsPassable = true;
                Appearance = AllChars.upstairChar;
                IsUpstair = true;
                isVisionBlocking = false;
                Color = ConsoleColor.Gray;
            }
            if (code == MapGenerator.keyplaceCode)
            {
                Appearance = '&'; //TEMPORARY
                IsPassable = true;
                switch (locklvl)
                {
                    case 0: Color = ConsoleColor.DarkMagenta; break;
                    case 1: Color = ConsoleColor.Green; break;
                    case 2: Color = ConsoleColor.DarkYellow; break;
                    default: Color = ConsoleColor.Green; break;
                }
                lockLevel = locklvl;
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
                Appearance = AllChars.openedDoorChar;
                return true;
            }
            return false;
        }

        public bool TryCloseDoor()
        {
            if (IsDoor && IsOpened)
            {
                IsOpened = false;
                IsPassable = false;
                isVisionBlocking = true;
                Appearance = AllChars.closedDoorChar;
                return true;
            }
            return false;
        }
    }
}
