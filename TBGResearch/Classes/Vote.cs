using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGResearch.Logic;

namespace TBGResearch.Classes
{
    public class Vote
    {
        /// <summary>
        /// Incoming vote 
        /// </summary>
        public string RawVote { get; set; }

        private Dictionary<TechTeam, ResearchFrame> _parsedVote = new Dictionary<TechTeam, ResearchFrame>();
        public Dictionary<TechTeam, ResearchFrame> ParsedVote
        {
            get
            {
                return _parsedVote;
            }
            set
            {
                _parsedVote = value;
            }
        }

        // === Methods ===

        public bool ProcessIncomingVote(string raw)
        {
            ParsedVote.Clear();
            VoteTranslator.Translate(this, raw);

            return ParsedVote.Count > 0;
        }
    }
}
