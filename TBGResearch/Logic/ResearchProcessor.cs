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
                ResearchResult turn = new ResearchResult() { Parent = nation, EntityId = nation.Name };

                // We only want live assignments, on the off-chance that a team has not been given an assignment, it does not need to be processed.
                foreach (KeyValuePair<TechTeam, ResearchFrame> allocation in nation.Assignments.Where(x => x.Value != null))
                {
                    ManageAllocation(nation, turn, allocation.Key, allocation.Value);
                }

                researchResults.Add(turn);
            }

            return researchResults;
        }

        private void ManageAllocation(Entity nation, ResearchResult turn, TechTeam researcher, ResearchFrame target)
        {
            ProgressFrame liveFrame = nation.Status.Frames.FirstOrDefault(x => x.IdTag == target.IdTag);
            if (liveFrame != null)
            {

                // Should this check for Active/Accessible frames, or leave that to the assignment routines?

                Tuple<ProgressFrame, List<String>> outcome =
                ProcessTeam(researcher, liveFrame,
                    BonusCalculator.CalculateEffectiveBonus(nation, researcher, target), // Get Admiral/Event Bonuses calculated for us
                    BonusCalculator.MatchPreference(researcher, target)); // And double check the skill match

                turn.UpdateFrames.Add(researcher, outcome.Item1); // Commit our now updated frame to the results

                if (researcher.IncrementXP(target.Tree)) // Next we process xp gain and see if the team has levelled up
                    turn.LevelledUpTeams.Add(researcher); // If yes, make sure we mark that down
            }
            else
                throw new ArgumentException("An invalid frame was provided");
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
                if (frame.Lines[i].WasCompleteInPreviousTurn) continue;
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
                int total = 0;
                foreach (var kv in remaining) total += kv.Value.Count;
                int r = rand.Next(total - 1);
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
               if (mnm.Key < -1 && !remainingIndices.ContainsKey(mnm.Key + 1)) remainingIndices.Add(mnm.Key + 1, new Queue<int>());
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
