using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class Attack
    {

        static int CalculateMeleeDamage(Unit attacker, Unit victim)
        {
            int MinMeleeDamage = attacker.Inv.Wielded.MinMeleeDamage;
            int MaxMeleeDamage = attacker.Inv.Wielded.MaxMeleeDamage;
            int baseDamage = MyRandom.getRandomInt(MinMeleeDamage, MaxMeleeDamage+1);
            int finalDamage = (int)(baseDamage + baseDamage * (attacker.Stats.Strength - 10)/10 * attacker.Inv.Wielded.StrengthFactor);
            if (finalDamage < 0) finalDamage = 0;
            return finalDamage;
        }

        public static void MeleeAttack(Unit attacker, Unit victim)
        {
            Weapon attackerWeapon = attacker.Inv.Wielded;
            if (attackerWeapon != null)
            {
                attacker.Timing.AddActionTime(TimeCost.MeleeAttackCost(attacker));
                int finalDamage = CalculateMeleeDamage(attacker, victim);
                bool victimStabbed = false;

                if (attacker.Inv.Wielded.TypeOfMeleeDamage == Weapon.MeleeDamageTypes.Stab && victim.IsUnaware())
                {
                    finalDamage *= 6; //zomg
                    Log.AddOneFromList(StringFactory.AttackerStabsVictim(attacker, victim));
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
                    Log.AddOneFromList(StringFactory.stabbedVictimReacts(victim));
                }
            }
        }

        static int CalculateRangedDamage(Unit attacker, Unit victim)
        {
            int MinDamage = attacker.Inv.Wielded.MinMeleeDamage;
            int MaxDamage = attacker.Inv.Wielded.MaxMeleeDamage;
            int baseDamage = MyRandom.getRandomInt(MinDamage, MaxDamage + 1);
            int finalDamage = baseDamage;
            if (finalDamage < 0) finalDamage = 0;
            return finalDamage;
        }

        public static void RangedAttack(Unit attacker, Unit victim)
        {
            Weapon attackerWeapon = attacker.Inv.Wielded;
            if (attackerWeapon.Range > 1)
            {
                attacker.Timing.AddActionTime(TimeCost.RangedAttackCost(attacker));
                if (!attackerWeapon.TryConsumeAmmo())
                {
                    if (attacker is Player)
                        Log.ReplaceLastLine("Click!");
                    else
                        Log.AddLine("Click!");
                    return;
                }
                int damage = CalculateRangedDamage(attacker, victim);
                victim.Hitpoints -= damage;
                if (attacker is Player)
                    Log.AddLine("You shot the " + victim.Name + " with your " + attacker.Inv.Wielded.DisplayName + "!");
                else
                    Log.AddLine(attacker.Name + " shoots at " + victim.Name + " with the " + attacker.Inv.Wielded.DisplayName + "!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                WorldRendering.DrawTraversingBullet(attacker.CoordX, attacker.CoordY, victim.CoordX, victim.CoordY, '*');
            }
            else
                _DEBUG.AddDebugMessage("ERROR: attempt to shoot from non-ranged weapon!");

        }

        public static void Strangle(Unit attacker, Unit victim)
        {
            if (victim.IsUnaware())
            {
                int KOtime = MyRandom.getRandomInt(50, 150);
                victim.KnockedOutTime += KOtime;
                if (attacker is Player)
                    Log.AddLine("You strangle " + victim.Name + "!");
            }
            else
                Log.AddLine(attacker.Name + " tried to grab and strangle " + victim.Name + ", but the victim was aware of it!");
        }

    }
}
