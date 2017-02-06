using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Key:Item
    {

        public int KeyLevel = 0;
        //{
        //get;
        //set
        //{
        //KeyLevel = value;
        //}
        //}

        public bool CanOpenLock(int lockLevel)
        {
            //_DEBUG.AddDebugMessage("Attempt to open the lock (LL "+lockLevel.ToString()+", KL "+KeyLevel.ToString());
            return KeyLevel == lockLevel;
        }

        public Key(int xc, int yc, int keylvl)
        {
            CoordX = xc;
            CoordY = yc;
            KeyLevel = keylvl;
            Appearance = AllChars.KeyChar;
            Color = AllChars.getKeyColor(KeyLevel);
            switch (KeyLevel)
            {
                case 1: name = "Arcane Key (" + Color.ToString() + ")"; break;
                case 2: name = "Arcane Key (" + Color.ToString() + ")"; break;
                case 3: name = "Arcane Key (" + Color.ToString() + ")"; break;
                default: name = "Arcane Key (" + Color.ToString() + ")"; break;
            }
        }


    }
}
