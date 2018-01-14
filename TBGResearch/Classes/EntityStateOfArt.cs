using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TBGResearch.Classes
{
    public class EntityStateOfArt
    {
        // Matching entity Id for matching purposes
        public string EntityId { get; set; }

        [JsonIgnore]
        public Entity Entity { get; set; }

        private List<ProgressFrame> _frames = new List<ProgressFrame>();
        /// <summary>
        /// A collection of all progress made
        /// </summary>
        public List<ProgressFrame> Frames
        {
            get
            {
                return _frames;
            }
            set
            {
                _frames = value;
            }
        }

        private List<ResearchBonus> _admiralBonuses = new List<ResearchBonus>();
        /// <summary>
        /// These are longer-term bonuses from either a national trait, technology, or officer.
        /// </summary>
        public List<ResearchBonus> AdmiralBonuses
        {
            get { return _admiralBonuses; }
            set { _admiralBonuses = value; }
        }

        private List<EventBonus> _eventBonuses = new List<EventBonus>();
        /// <summary>
        /// These are typically one off bonuses for a specific tag.
        /// </summary>
        public List<EventBonus> EventBonuses
        {
            get { return _eventBonuses; }
            set { _eventBonuses = value; }
        }

        // === Methods ===

        public void Init ()
        {
            // TODO: Acquire Entity based on EntityId
        }

        public static EntityStateOfArt Deserialise(string serial)
        {
            EntityStateOfArt art = JsonConvert.DeserializeObject<EntityStateOfArt>(serial);
            art.Init();
            return art;
        }
    }
}
 