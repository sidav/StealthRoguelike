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

        public int Strength, Nerve, Endurance, Agility, Knowledge;
        //stats are from 1 to 100 (inclusive). 
        //100 is superhuman stat, maybe

        public SneakRoleplayStats() //"base human" stats
        {
            Strength = Algorithms.getRandomInt(30,50);
            Nerve = Algorithms.getRandomInt(30, 50);
            Endurance = Algorithms.getRandomInt(30, 50);
            Agility = Algorithms.getRandomInt(30, 50);
            Knowledge = Algorithms.getRandomInt(30, 50);
        }

        public SneakRoleplayStats(int s, int n, int e, int a, int k)
        {
            Strength = s;
            Nerve = n;
            Endurance = e;
            Agility = a;
            Knowledge = k;
        }


    }
}
