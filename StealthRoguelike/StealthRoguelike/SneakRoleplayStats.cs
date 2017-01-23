using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class SneakRoleplayStats //Awesome. Innovative. Unique. S.N.E.A.K. roleplay system!
    {
        //Every formula is not final and should be tested!
        public int Level;
        public int Strength, Nerve, Endurance, Agility, Knowledge;
        //stats are from 1 to 20 (inclusive). 
        //20 is superhuman stat, maybe

        public SneakRoleplayStats() //"base human" stats
        {
            Level = 1;
            Strength = LCGRandom.getRandomInt(8, 13);
            Nerve = LCGRandom.getRandomInt(8, 13);
            Endurance = LCGRandom.getRandomInt(8, 13);
            Agility = LCGRandom.getRandomInt(8, 10);
            Knowledge = LCGRandom.getRandomInt(8, 13);
        }

        public SneakRoleplayStats(int s, int n, int e, int a, int k)
        {
            Level = 1;
            Strength = s;
            Nerve = n;
            Endurance = e;
            Agility = a;
            Knowledge = k;
        }


    }
}
