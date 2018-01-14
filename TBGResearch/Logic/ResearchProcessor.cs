using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGResearch.Classes;

namespace TBGResearch.Logic
{
    public class ResearchProcessor
    {
        /// <summary>
        /// The entry point of the processing routine.
        /// </summary>
        /// <returns>The list of all research results</returns>
        public List<ResearchResult> ProcessMaster()
        {
            List<ResearchResult> researchResults = new List<ResearchResult>();

            foreach (Entity nation in Master.MasterEntityList)
            {
                // We only want live assignments, on the off-chance that a team has not been given an assignment, it does not need to be processed.
                foreach (KeyValuePair<TechTeam, ResearchFrame> allocation in nation.Assignments.Where(x => x.Value != null))
                {
                    ProgressFrame liveFrame = nation.Status.Frames.First(x => x.IdTag == allocation.Value.IdTag);

                    // Should this check for Active/Accessible frames, or leave that to the assignment routines?

                    ProcessTeam(allocation.Key, liveFrame, 
                        BonusCalculator.CalculateEffectiveBonus(nation, allocation.Key, allocation.Value), // Get Admiral/Event Bonuses calculated for us
                        BonusCalculator.MatchPreference(allocation.Key, allocation.Value)); // And double check the skill match
                }
            }

            return researchResults;
        }

        private Tuple<ProgressFrame, List<String>> ProcessTeam(TechTeam team, ProgressFrame frame, int bonus, bool matchesSkill)
        {
            List<String> rewards = new List<String>();
            int minProgress = bonus + team.SkillLevel * (matchesSkill ? 2 : 1);
            int overflow = 0;
            SortedDictionary<int, Queue<int>> remaining = new SortedDictionary<int, Queue<int>>();
            List<ComponentProgressLine> back = new List<ComponentProgressLine>();
            for (int i = 0; i < frame.Lines.Count; ++i)
            {
                int diff = frame.Lines[i].Advance(minProgress);
                if (diff >= 0)
                {
                    overflow += diff;
                    rewards.Add(frame.Lines[i].Line.Reward);
                }
                else
                {
                    if (remaining.ContainsKey(diff))
                        remaining[diff].Enqueue(i);
                    else
                    {
                        var q = new Queue<int>();
                        q.Enqueue(i);
                        remaining.Add(diff, q);
                    }
                }
            }
            AssignOverflow(frame, remaining, rewards, overflow);
            Random rand = new Random();
            int inspirationAndBoost = 1;
            if (team.AssignedBoosts > 0)
            {
                inspirationAndBoost += 1;
                team.AssignedBoosts -= 1;
            }
            for (int j = 0; j < inspirationAndBoost; ++j)
            {
                int r = rand.Next(frame.Lines.Count - rewards.Count - 1);
                foreach(var kv in remaining)
                {
                    if (kv.Value.Count < r) r -= kv.Value.Count;
                    else
                    {
                        int i = kv.Value.Dequeue();
                        if (r > 0)
                        {
                            kv.Value.Enqueue(i);
                            --r;
                        }
                        else
                        {
                            int diff = frame.Lines[i].Advance(5);
                            if (diff >= 0)
                            {
                                rewards.Add(frame.Lines[i].Line.Reward);
                                AssignOverflow(frame, remaining, rewards, diff);
                            }
                            else
                            {
                                if (remaining.ContainsKey(diff))
                                    remaining[diff].Enqueue(i);
                                else
                                {
                                    var q = new Queue<int>();
                                    q.Enqueue(i);
                                    remaining.Add(diff, q);
                                }
                            }
                            break;
                        }
                    }
                }
            }        
            return new Tuple<ProgressFrame, List<String>>(frame, rewards);
        }

        private void AssignOverflow(ProgressFrame frame, SortedDictionary<int, Queue<int>> remainingIndices, List<String> rewards, int overflow)
        {
            while (overflow > 0 && remainingIndices.Count > 0)
            {
               var mnm = remainingIndices.Min();
               if (mnm.Key > 1 && !remainingIndices.ContainsKey(mnm.Key + 1)) remainingIndices.Add(mnm.Key + 1, new Queue<int>());
               while (mnm.Value.Count > 0 && overflow > 0)
               {
                   --overflow;
                   int i = mnm.Value.Dequeue();
                   int diff = frame.Lines[i].Advance(1);
                   if (diff >= 0) rewards.Add(frame.Lines[i].Line.Reward);
                   else remainingIndices[diff].Enqueue(i);
               }
               if (mnm.Value.Count == 0) remainingIndices.Remove(mnm.Key);
            }
        }
    }
}
