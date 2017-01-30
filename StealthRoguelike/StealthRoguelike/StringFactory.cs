using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealthRoguelike
{
    class StringFactory
    {

        public static List<string> AttackerStabsVictim(Unit attacker, Unit victim)
        {
            List<string> res = new List<string>();
            if (attacker is Player)
            {
                res.Add("You silently stabbed " + victim.Name + "'s neck with your " + attacker.Inv.Wielded.DisplayName + "!!");
                res.Add("You skewer " + victim.Name + " like a kebab!!");
            }
            else
            {
                res.Add(attacker.Name + " stabs " + victim.Name + " with the " + attacker.Inv.Wielded.DisplayName + "!");
            }
            return res;
        }

        public static List<string> stabbedVictimReacts(Unit victim)
        {
            List<string> res = new List<string>();
            res.Add(victim.Name + " convulces in agony!");
            res.Add(victim.Name + " chokes his blood!");
            return res;
        }
    }
}
