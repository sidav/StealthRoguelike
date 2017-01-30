using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class TimeCost //Needed to calculate time cost in turns for any unit action
    {

        public static int SkipTurnCost(Unit acting)
        {
            return 10;
        }

        public static int MoveCost(Unit acting)
        {
            int agi = acting.Stats.Agility;
            double encumbrance = acting.Inv.getEncumbrance();
            double endurance = acting.Stats.Endurance;
            double encumbModifier;

            double baseCost = 10 + (10 - agi) / 2;

            if (encumbrance == 0)
                encumbModifier = 1;
            else
                encumbModifier = (10/endurance) * (1 + encumbrance / 2); //if encumbered

            double finalCost = baseCost * encumbModifier;
            return (int)Math.Round(finalCost);
        }

        public static int OpenDoorCost(Unit acting)
        {
            return 10;
        }

        public static int CloseDoorCost(Unit acting)
        {
            return 10;
        }

        public static int AttackCost(Unit acting)
        {
            return 10;
        }

        public static int PeepCost(Unit acting)
        {
            return 15;
        }

        public static int ContinuePeepCost(Unit acting)
        {
            return 10;
        }

        public static int StrangleCost(Unit acting)
        {
            return MyRandom.getRandomInt(15, 25);
        }

        public static int TurningCost(Unit acting)
        {
            return 3;
        }

        public static int PickUpCost(Item picking)
        {
            return picking.GetWeight() + 5;
        }

        public static int DropItemCost(Item dropped)
        {
            return dropped.GetWeight() / 2;
        }

        //Next methods are actor-only routines
        public static int GuardWait(Actor acting) //I just can't think of better name for this
        {
            return 4;
        }

    }
}
