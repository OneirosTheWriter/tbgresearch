using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TBGResearch.Classes
{
    public class TechPriority
    {
        /// <summary>
        /// The Current stat of research progress.
        /// </summary>
        [JsonIgnore]
        public EntityStateOfArt Status { get; set; }

        /// <summary>
        /// Additional Priority assigned to chasing landmark techs
        /// </summary>
        public double LandmarkTechPriority { get; set; }

        private Dictionary<PartType, double> _PartTechPriority = new Dictionary<PartType, double>();
        /// <summary>
        /// A list of special part priorities. If not found in here, priority is 1.
        /// </summary>
        public Dictionary<PartType, double> PartTechPriority
        {
            get
            {
                return _PartTechPriority;
            }
            set
            {
                _PartTechPriority = value;
            }
        }

        // === Methods ===

        public double GetPartPriority(PartType type)
        {
            if (PartTechPriority.ContainsKey(type))
                return PartTechPriority[type];
            else
                return 1;
        }

        public double AssignPriority(ResearchFrame target)
        {
            throw new NotImplementedException();
        }
    }
}
