using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Attack
    {

        public static void dealDamage(Unit attacker, Unit victim)
        {
            if (attacker.Wielded != null)
            {
                int mindamage = attacker.Wielded.mindamage;
                int maxdamage = attacker.Wielded.maxdamage;
                int finalDamage = Algorithms.getRandomInt(mindamage, maxdamage);
                if (attacker.Wielded.TypeOfDamage == Weapon.damageTypes.stab && victim.IsUnaware())
                {
                    finalDamage *= 2;
                    if (attacker is Player)
                        Log.AddLine("You silently stabbed " + victim.Name + "'s neck with your " + attacker.Wielded.Name + "!!");
                    else
                        Log.AddLine(attacker.Name + " stabs " + victim.Name + " with the " + attacker.Wielded.Name + "!");
                }
                else
                {
                    if (attacker is Player)
                        Log.AddLine("You hit " + victim.Name + " with your " + attacker.Wielded.Name + "!");
                    else
                        Log.AddLine(attacker.Name + " hits " + victim.Name + " with the " + attacker.Wielded.Name + "!");
                }
                victim.Hitpoints -= finalDamage;
            }
        }

        public static void KnockOut(Unit attacker, Unit victim)
        {
            if (victim.IsUnaware())
            {
                int kotime = Algorithms.getRandomInt(50, 150);
                victim.KnockedOutTime += 150;
                if (attacker is Player)
                    Log.AddLine("You strangle " + victim.Name + "!");
            }
        }

    }
}
