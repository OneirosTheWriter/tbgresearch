using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TBGResearch.Classes
{
    public struct ComponentLine
    {
        public int RequiredResearch;
        public string ShortName;
        public string FlavourName;
        public string Reward;
    }

    public class ComponentProgressLine
    {
        public bool WasComplete;
        public int Progress;
        public ComponentLine Line;
    }

    public class ResearchFrame
    {
        /// <summary>
        /// Unique Id for this Frame
        /// </summary>
        public string IdTag { get; set; }

        /// <summary>
        /// Name of the Frame
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The subtitle text.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Here for ease of updating priorities
        /// </summary>
        public TechTreeType Tree { get; set; }

        /// <summary>
        /// Is this a landmark text like Isolinear?
        /// </summary>
        public bool IsLandmark { get; set; }

        private List<ComponentLine> _lines = new List<ComponentLine>();
        /// <summary>
        /// Collection of line items that belong to this frame.
        /// </summary>
        public List<ComponentLine> Lines
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

        private List<string> _Prereqs = new List<string>();
        /// <summary>
        /// A list of Frame itemTags that are prereqs. Used for importing/exporting
        /// </summary>
        public List<string> Prereqs
        {
            get
            {
                return _Prereqs;
            }
            set
            {
                _Prereqs = value;
            }
        }

        private List<ResearchFrame> _prereqFrames = new List<ResearchFrame>();
        /// <summary>
        /// Easier to use list of frames.
        /// </summary>
        [JsonIgnore]       
        public List<ResearchFrame> PrereqFrames
        {
            get
            {
                return _prereqFrames;
            }
        }

        private List<string> _LeadsTo = new List<string>();
        /// <summary>
        /// A list of Frame itemTags that are unlocked by this. Used for importing/exporting
        /// </summary>
        public List<string> LeadsTo
        {
            get
            {
                return _LeadsTo;
            }
            set
            {
                _LeadsTo = value;
            }
        }

        private List<ResearchFrame> _leadsToFrames = new List<ResearchFrame>();
        /// <summary>
        /// Easier to use list of frames.
        /// </summary>
        [JsonIgnore]
        public List<ResearchFrame> LeadsToFrames
        {
            get
            {
                return _leadsToFrames;
            }
        }

        private List<PartType> _PartContained = new List<PartType>();
        public List<PartType> PartContained
        {
            get { return _PartContained; }
            set { _PartContained = value; }
        }

        /// <summary>
        /// The description line of what this will unlock
        /// </summary>
        public string Result { get; set; }
    }
}
