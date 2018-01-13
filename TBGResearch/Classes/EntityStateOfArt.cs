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
