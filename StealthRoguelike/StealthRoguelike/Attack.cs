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
            int baseDamage = MyRandom.getRandomInt(mindamage, maxdamage+1);
            int finalDamage = (int)(baseDamage + baseDamage * (attacker.Stats.Strength - 10)/10 * attacker.Inv.Wielded.StrengthFactor);
            if (finalDamage < 0) finalDamage = 0;
            return finalDamage;
        }

        public static void MeleeAttack(Unit attacker, Unit victim)
        {
            if (attacker.Inv.Wielded != null)
            {                
                int finalDamage = CalculateDamage(attacker, victim);
                bool victimStabbed = false;

                if (attacker.Inv.Wielded.TypeOfMeleeDamage == Weapon.MeleeDamageTypes.Stab && victim.IsUnaware())
                {
                    finalDamage *= 6; //zomg
                    if (attacker is Player)
                        Log.AddLine("You silently stabbed " + victim.Name + "'s neck with your " + attacker.Inv.Wielded.DisplayName + "!!");
                    else
                        Log.AddLine(attacker.Name + " stabs " + victim.Name + " with the " + attacker.Inv.Wielded.DisplayName + "!");
                    victimStabbed = true;
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
                    _DEBUG.AddDebugMessage(" " + finalDamage.ToString() + " damage");

                if (victim is Player)
                {
                    Gameover.KilledBy = attacker.Name;
                    if (victim.Hitpoints < victim.GetMaxHitpoints() / 3 || victim.Hitpoints < 3)
                        Log.AddAlertMessage("!!LOW HITPOINT WARNING!!");
                }
                if (victimStabbed)
                {
                    List<string> stabbedMsgs = new List<string>();
                    stabbedMsgs.Add(victim.Name + " convulces in agony!");
                    stabbedMsgs.Add(victim.Name + " chokes his blood!");
                    Log.AddOneFromList(stabbedMsgs);
                }
            }
        }

        public static void Strangle(Unit attacker, Unit victim)
        {
            if (victim.IsUnaware())
            {
                int kotime = MyRandom.getRandomInt(50, 150);
                victim.KnockedOutTime += 150;
                if (attacker is Player)
                    Log.AddLine("You strangle " + victim.Name + "!");
            }
            else
                Log.AddLine(attacker.Name + " tried to grab and strangle " + victim.Name + ", but the victim was aware of it!");
        }

    }
}
