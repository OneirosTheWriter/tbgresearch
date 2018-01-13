using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBGResearch.Classes
{
    /// <summary>
    /// Global class bearing the master lists for Entities and TechTrees.
    /// </summary>
    public static class Master
    {
        private static List<Entity> _masterEntityList = new List<Entity>();
        /// <summary>
        /// The global list for Entities
        /// </summary>
        public static List<Entity> MasterEntityList
        {
            get
            {
                return _masterEntityList;
            }
            set
            {
                _masterEntityList = value;
            }
        }

        private static List<TechTree> _masterTreeList = new List<TechTree>();
        /// <summary>
        /// The global list for Tech Trees
        /// </summary>
        public static List<TechTree> MasterTreeList
        {
            get
            {
                return _masterTreeList;
            }
            set
            {
                _masterTreeList = value;
            }
        }
    }
}
