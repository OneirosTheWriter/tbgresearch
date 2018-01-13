using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TBGResearch.Classes
{
    public class ResearchResult
    {
        /// <summary>
        /// The Id of the Entity this result is for
        /// </summary>
        public string EntityId { get; set; }

        [JsonIgnore]
        public Entity Parent { get; set; }

        private Dictionary<TechTeam, ProgressFrame> _updatedFrames = new Dictionary<TechTeam, ProgressFrame>();
        /// <summary>
        /// These are the frames that have been touched in this research cycle
        /// </summary>
        public Dictionary<TechTeam, ProgressFrame> UpdateFrames
        {
            get { return _updatedFrames; }
            set { _updatedFrames = value; }
        }

        private List<TechTeam> _levelledUpTeams = new List<TechTeam>();
        /// <summary>
        /// These are the teams that have increased their level in the latest round of research
        /// </summary>
        public List<TechTeam> LevelledUpTeams
        {
            get { return _levelledUpTeams; }
            set { _levelledUpTeams = value; }
        }
    }
}
