using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TBGResearch.Classes;

namespace TBGResearch.Logic
{
    /// <summary>
    /// Static class for calculating any bonuses that belong to a given frame.
    /// </summary>
    public static class BonusCalculator
    {
        /// <summary>
        /// Total up the three relevant sources of bonuses.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="researcher"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static int CalculateEffectiveBonus(Entity parent, TechTeam researcher, ResearchFrame frame)
        {
            int bonus = parent.Status.AdmiralBonuses.Where(x => x.TreeTarget == frame.Tree)?.Sum(x => x.BonusValue) ?? 0;
            bonus += parent.Status.AdmiralBonuses.Where(x => frame.PartContained.Contains(x.PartTarget))?.Sum(x => x.BonusValue) ?? 0;
            bonus += parent.Status.EventBonuses.Where(x => x.FrameTag == frame.IdTag)?.Sum(x => x.BonusForAllLines) ?? 0;
            
            return bonus;
        }

        /// <summary>
        /// Run the check to see if this matches the research preferences.
        /// </summary>
        /// <param name="researcher">TechTeam being checked</param>
        /// <param name="frame">Frame being researched</param>
        /// <returns></returns>
        public static bool MatchPreference(TechTeam researcher, ResearchFrame frame)
        {
            if (researcher.PreferredSkill1 == frame.Tree || researcher.PreferredSkill2 == frame.Tree)
                return true;
            else
                return false;
        }
    }
}
