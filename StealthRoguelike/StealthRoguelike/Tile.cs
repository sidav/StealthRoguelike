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
        public bool IsPassable, isVisionBlocking, IsDoor, IsOpened, IsLocked, IsUpstair, IsDownstair, isKeyPlace;
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
                Color = AllChars.getKeyColor(locklvl);
                lockLevel = locklvl;
                IsLocked = (lockLevel > 0);
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
                Appearance = AllChars.keyPlaceChar;
                IsPassable = true;
                Color = AllChars.getKeyColor(locklvl);
                lockLevel = locklvl;
                isKeyPlace = true;
            }
        }

        public bool TryUnlockDoor(List<Key> keys)
        {
            if (IsDoor && IsLocked)
                foreach (Key k in keys)
                    if (k.CanOpenLock(lockLevel))
                    {
                        IsLocked = false;
                        return true;
                    }
            return false;
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
