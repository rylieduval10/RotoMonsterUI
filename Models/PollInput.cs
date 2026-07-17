using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PollOptionInput
    {
        public int OptionId { get; set; }
        public string Label { get; set; }
        public int VoteCount { get; set; }
        public bool IsSelectedByUser { get; set; }
        public List<DisplayPlayerInput> Players { get; set; } = new List<DisplayPlayerInput>();
    }

    public class PollInput
    {
        public int PollId { get; set; }
        public string CreatorUsername { get; set; }
        public TimeSpan? TimeSinceCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        public TimeSpan? PollLength { get; set; }
        public string Question { get; set; }
        public LeagueSettingsInput LeagueSettings { get; set; }
        public List<PollOptionInput> Options { get; set; } = new List<PollOptionInput>();
        public bool UserCanDelete { get; set; }
        public bool UserCanEditPlayers { get; set; } = true;
        public bool IsEditingPlayers { get; set; }
        public int? EditingOptionId { get; set; }

        public int TotalVoteCount
        {
            get
            {
                var sum = 0;
                foreach (var o in Options) sum += o.VoteCount;
                return sum;
            }
        }
    }
}