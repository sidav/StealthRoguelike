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
            if (attacker.Inv.Wielded != null)
            {
                int mindamage = attacker.Inv.Wielded.mindamage;
                int maxdamage = attacker.Inv.Wielded.maxdamage;
                int finalDamage = Algorithms.getRandomInt(mindamage, maxdamage);
                if (attacker.Inv.Wielded.TypeOfDamage == Weapon.damageTypes.stab && victim.IsUnaware())
                {
                    finalDamage *= 2;
                    if (attacker is Player)
                        Log.AddLine("You silently stabbed " + victim.Name + "'s neck with your " + attacker.Inv.Wielded.Name + "!!");
                    else
                        Log.AddLine(attacker.Name + " stabs " + victim.Name + " with the " + attacker.Inv.Wielded.Name + "!");
                }
                else
                {
                    if (attacker is Player)
                        Log.AddLine("You hit " + victim.Name + " with your " + attacker.Inv.Wielded.Name + "!");
                    else
                        Log.AddLine(attacker.Name + " hits " + victim.Name + " with the " + attacker.Inv.Wielded.Name + "!");
                }

                victim.Hitpoints -= finalDamage;

                if (victim is Player)
                {
                    Gameover.KilledBy = attacker.Name;
                    if (victim.Hitpoints < victim.MaxHitpoints / 3 || victim.Hitpoints < 3)
                        Log.AddWarning("!!LOW HITPOINT WARNING!!");
                }
            }
        }

        public static void Strangle(Unit attacker, Unit victim)
        {
            if (victim.IsUnaware())
            {
                int kotime = Algorithms.getRandomInt(50, 150);
                victim.KnockedOutTime += 150;
                if (attacker is Player)
                    Log.AddLine("You strangle " + victim.Name + "!");
            }
            else
                Log.AddLine(attacker.Name + " tried to grab and strangle " + victim.Name + ", but the victim was aware of it!");
        }

    }
}
