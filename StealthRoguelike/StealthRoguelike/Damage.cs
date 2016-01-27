using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Damage
    {

        public static void dealDamage(Unit attacker, Unit target)
        {
            if (attacker.wielded != null)
            {
                int mindamage = attacker.wielded.mindamage;
                int maxdamage = attacker.wielded.maxdamage;
                int finalDamage = Algorithms.getRandomInt(mindamage, maxdamage);
                target.Hitpoints -= finalDamage;
                Log.AddLine(attacker.Name + " hits " + target.Name + " with the " +attacker.wielded.name+"!");
            }
        }

    }
}
