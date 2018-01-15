using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGResearch.Classes
{
    public class TechTeam
    {
        /// <summary>
        /// Unique id for the Tech Team
        /// </summary>
        public string IdTag { get; set; }

        /// <summary>
        /// The name of the Tech Team
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If a Logo exists, store the link here.
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// The current skill level of the Team.
        /// </summary>
        public int SkillLevel { get; set; }

        /// <summary>
        /// The first Preferred area of the Team.
        /// </summary>
        public TechTreeType PreferredSkill1 { get; set; }

        /// <summary>
        /// The second Preferred area of the Team. This may be -1 if only one preferred area exists.
        /// </summary>
        public TechTreeType PreferredSkill2 { get; set; }

        /// <summary>
        /// The current Experience of the Team accumulated to the next level.
        /// </summary>
        public int Experience { get; set; }
        /// <summary>
        /// Whether the team is a generic team that needs to gain experience to graduate to a qualified team and earn preferred skills.
        /// </summary>
        public bool IsGeneric { get; set; }

        private Dictionary<TechTreeType, int> _workedIn = new Dictionary<TechTreeType, int>();

        /// <summary>
        /// What areas a generic team worked in, and how often, to determine what preferred skills it will graduate with.
        /// Not needed for qualifed (non-generic) teams.
        /// </summary>
        public Dictionary<TechTreeType, int> WorkedIn
        {
            get
            {
                return WorkedIn;
            }
            set
            {
                WorkedIn = value;
            }
        }
        /// <summary>
        /// The number of Boosts currently pending for this team.
        /// </summary>
        public int AssignedBoosts { get; set; }

        // === Methods ===

        /// <summary>
        /// Increase the XP count
        /// </summary>
        /// <returns></returns>
        public bool IncrementXP(TechTreeType type)
        {
            bool isPromoted = false;
            Experience++;
            if (IsGeneric)
            {
                WorkedIn.TryGetValue(type, out int not_used);
                WorkedIn[type]++;
                if (Experience >= 10)
                {
                    SkillLevel++;
                    IsGeneric = false;
                    isPromoted = true;
                    var preferred = WorkedIn.OrderByDescending(kv => kv.Value).Take(2);

                    // If the team earned 10 XP it must have worked in at least one area.
                    PreferredSkill1 = preferred.First().Key;
                    if (preferred.Count() > 1) PreferredSkill2 = preferred.ElementAt(1).Key;
                }
            }
            else if (Experience > (SkillLevel + 1)) // Rather than store an individual variable, we'll just check against this, which is a quick op anyway
            {
                SkillLevel++; // Increment level and reset XP back to 0
                Experience = 0;
                isPromoted = true;
            }

            return isPromoted;
        }
    }
}
