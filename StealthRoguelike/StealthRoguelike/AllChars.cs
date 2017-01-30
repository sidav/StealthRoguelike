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

        //Weapons
        public const char meleeWeaponChar = ')';
        public const char firearmChar = '/';

        //Other
        public const char AmmunitionChar = '"';
    }
}
