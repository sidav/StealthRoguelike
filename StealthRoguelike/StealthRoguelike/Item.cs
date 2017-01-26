﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Item //Any thing that can be picked up.
    {
        protected string name;
        public string DisplayName
        {
            get
            {
                if (isStackable && Quantity > 1)
                    return Quantity.ToString() + " " + name + "s";
                else
                    return name;
            }
        }
        protected int weight = 1;
        public int Quantity = 1;
        public bool isStackable = true;
        public int CoordX, CoordY;
        public char Appearance;
        public ConsoleColor Color;

        public int GetWeight()
        {
            return weight * Quantity;
        }

        public bool tryMergeStacks(Item someItem)
        {
            if (isEqualTo(someItem))
            {
                this.Quantity += someItem.Quantity;
                return true;
            }
            return false;

        }

        public bool isEqualTo(Item someItem)
        {
            if (this.name == someItem.name)
                return true;
            return false;
        }

        public void Draw()
        {
            ConsoleColor currcolor = Console.ForegroundColor;
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(CoordX, CoordY);
            Console.Write(Appearance);
            Console.ForegroundColor = currcolor;
        }
    }
}
