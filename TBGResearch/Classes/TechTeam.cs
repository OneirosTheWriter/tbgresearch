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
        /// The number of Boosts currently pending for this team.
        /// </summary>
        public int AssignedBoosts { get; set; }

        // === Methods ===

        /// <summary>
        /// Increase the XP count
        /// </summary>
        /// <returns></returns>
        public bool IncrementXP()
        {
            throw new NotImplementedException();
        }
    }
}
