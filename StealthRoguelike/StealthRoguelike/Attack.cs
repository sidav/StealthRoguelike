using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Attack
    {

        static int CalculateDamage(Unit attacker, Unit victim)
        {
            int mindamage = attacker.Inv.Wielded.mindamage;
            int maxdamage = attacker.Inv.Wielded.maxdamage;
            int baseDamage = LCGRandom.getRandomInt(mindamage, maxdamage+1);
            int finalDamage = (int)(baseDamage + baseDamage * (attacker.Stats.Strength - 10)/10 * attacker.Inv.Wielded.StrengthFactor);
            if (finalDamage < 0) finalDamage = 0;
            return finalDamage;
        }

        public static void dealDamage(Unit attacker, Unit victim)
        {
            if (attacker.Inv.Wielded != null)
            {
                
                int finalDamage = CalculateDamage(attacker, victim);
                if (attacker.Inv.Wielded.TypeOfDamage == Weapon.damageTypes.Stab && victim.IsUnaware())
                {
                    finalDamage *= 6; //zomg
                    if (attacker is Player)
                        Log.AddLine("You silently stabbed " + victim.Name + "'s neck with your " + attacker.Inv.Wielded.DisplayName + "!!");
                    else
                        Log.AddLine(attacker.Name + " stabs " + victim.Name + " with the " + attacker.Inv.Wielded.DisplayName + "!");
                }
                else
                {
                    if (attacker is Player)
                        Log.AddLine("You hit " + victim.Name + " with your " + attacker.Inv.Wielded.DisplayName + "!");
                    else
                        Log.AddLine(attacker.Name + " hits " + victim.Name + " with the " + attacker.Inv.Wielded.DisplayName + "!");
                }

                victim.Hitpoints -= finalDamage;
                if (attacker is Player)
                    Log.AddDebugMessage(" " + finalDamage.ToString() + " damage");

                if (victim is Player)
                {
                    Gameover.KilledBy = attacker.Name;
                    if (victim.Hitpoints < victim.GetMaxHitpoints() / 3 || victim.Hitpoints < 3)
                        Log.AddAlertMessage("!!LOW HITPOINT WARNING!!");
                }
            }
        }

        public static void Strangle(Unit attacker, Unit victim)
        {
            if (victim.IsUnaware())
            {
                int kotime = LCGRandom.getRandomInt(50, 150);
                victim.KnockedOutTime += 150;
                if (attacker is Player)
                    Log.AddLine("You strangle " + victim.Name + "!");
            }
            else
                Log.AddLine(attacker.Name + " tried to grab and strangle " + victim.Name + ", but the victim was aware of it!");
        }

    }
}
