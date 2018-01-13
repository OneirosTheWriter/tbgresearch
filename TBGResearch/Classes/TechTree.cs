using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGResearch.Classes
{
    public class TechTree
    {
        /// <summary>
        /// The name of this tech tree
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of this tree, used for matching Team Prefs.
        /// </summary>
        public TechTreeType TreeType { get; set; }

        [JsonIgnore]
        /// <summary>
        /// The initial "Turn of the Century" frame.
        /// </summary>
        public ResearchFrame RootFrame { get; set; }

        /// <summary>
        /// Master List, maintained mainly for importing and exporting
        /// </summary>
        List<ResearchFrame> MasterFrameList { get; set; }

        private Dictionary<string, ResearchFrame> TagFrameMap = new Dictionary<string, ResearchFrame>();

        // === Methods ===

        /// <summary>
        /// Return a Frame based upon the provided idTag.
        /// </summary>
        /// <param name="idTag">Requested Frame id</param>
        /// <returns></returns>
        public ResearchFrame Find(string idTag)
        {
            if (TagFrameMap.ContainsKey(idTag))
                return TagFrameMap[idTag];
            else
                return null;
        }

        private void PopulateLists()
        {
            throw new NotImplementedException();
        }
    }
}
