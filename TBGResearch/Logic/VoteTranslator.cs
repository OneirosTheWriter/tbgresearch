using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TBGResearch.Classes;

namespace TBGResearch.Logic
{
    public static class VoteTranslator
    {
        public static void Translate(Vote vote, string raw)
        {
            Dictionary<TechTeam, ResearchFrame> parsedVote = new Dictionary<TechTeam, ResearchFrame>();

            /* Assumes Federation will be first Entity listed, and the tech tree used by the Federation the first tech tree listed.
               Otherwise the Entity needs to be passed in or defined in a static member somewhere, and the Entity needs to store the
               tech tree it is using. */

            TechTree usedTree = Master.MasterTreeList[0];
            Entity votedOn = Master.MasterEntityList[0];
            using (var sr = new System.IO.StringReader(raw))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(":"))
                    {
                        string[] split = line.Split(':');
                        int start = split[0].LastIndexOf(']') +1;
                        string team = split[0].Substring(start, split[0].Length - start);
                        string newProject;
                        if (split[1].Contains("->"))
                        {
                            string[] projects = split[1].Split(new[] { "->" }, StringSplitOptions.None);
                            newProject = projects[1];
                        }
                        else newProject = split[1];
                        team.Trim();
                        newProject.Trim();
                        TechTeam techTeam = votedOn.Teams.Find(tt => tt.IdTag == team);
                        ResearchFrame frame = usedTree.Find(newProject);
                        if (techTeam != null && frame != null)
                        {
                            parsedVote.Add(techTeam, frame);
                        }
                    }
                    else if (line.Contains("[BOOST]"))
                    {
                        int start = line.LastIndexOf(']') +1;
                        string[] teams = line.Substring(start, line.Length - start).Split(',');
                        foreach (String team in teams)
                        {
                            team.Trim();
                            TechTeam techTeam = votedOn.Teams.Find(tt => tt.IdTag == team);
                            techTeam.AssignedBoosts += 1;
                        }

                    }
                }
            }
        }
    }
}
