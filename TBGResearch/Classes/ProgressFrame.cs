using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TBGResearch.Classes
{
    public class ProgressFrame
    {
        /// <summary>
        /// Identifying detail
        /// </summary>
        public string IdTag { get; set; }

        /// <summary>
        /// Has this Frame been completed already?
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Is this Frame able to be selected?
        /// </summary>
        public bool IsAccessible { get; set; }

        /// <summary>
        /// Does this frame have a Tech Team assigned?
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// If a team is assigned, store the Team Id here
        /// </summary>
        public string CurrentTechTeamId { get; set; }

        /// <summary>
        /// A reference to the Tech Team, not saved to Json.
        /// </summary>
        [JsonIgnore]
        public TechTeam CurrentTechTeam { get; set; }

        [JsonIgnore]
        public ResearchFrame ParentFrame { get; set; }

        private List<ComponentProgressLine> _lines = new List<ComponentProgressLine>();
        /// <summary>
        /// Lines that belong to this frame, with progress values.
        /// </summary>
        public List<ComponentProgressLine> Lines
        {
            get
            {
                return _lines;
            }
            set
            {
                _lines = value;
            }
        }
    }
}
