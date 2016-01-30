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
            if (attacker.Wielded != null)
            {
                int mindamage = attacker.Wielded.mindamage;
                int maxdamage = attacker.Wielded.maxdamage;
                int finalDamage = Algorithms.getRandomInt(mindamage, maxdamage);
                target.Hitpoints -= finalDamage;
                if (attacker is Player)
                    Log.AddLine("You hit " + target.Name + " with your " + attacker.Wielded.Name + "!");
                else 
                    Log.AddLine(attacker.Name + " hits " + target.Name + " with the " +attacker.Wielded.Name+"!");
            }
        }

    }
}
