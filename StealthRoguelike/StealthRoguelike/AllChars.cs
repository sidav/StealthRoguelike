using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class AllChars //just holds all the char constants, for their quick remplacement if needed 
    {
        //Tiles
        public const char wallChar = '▒';//'#';
        public const char closedDoorChar = '+';
        public const char openedDoorChar = '\\';
        public const char floorChar = '.';
        public const char upstairChar = '<';
        public const char downstairChar = '>';
        public const char keyPlaceChar = ':';

        //Weapons
        public const char meleeWeaponChar = ')';
        public const char firearmChar = '/';

        //Other
        public const char AmmunitionChar = '"';
        public const char KeyChar = '\'';

        //KEY AND RELATED THINGS COLORS:
        public static ConsoleColor getKeyColor(int keyLevel)
        {
            switch (keyLevel)
            {
                case 0: return ConsoleColor.DarkMagenta;
                case 1: return ConsoleColor.DarkGreen;
                case 2: return ConsoleColor.DarkCyan;
                case 3: return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }




    }
}
