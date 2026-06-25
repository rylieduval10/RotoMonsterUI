using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class LineupCardTeamInput
    {
        public string TeamCode { get; set; }
        public int? Rank { get; set; }
        public float ProjectedRuns { get; set; }
        public string OddsLine { get; set; }
        public string PlayerVsHandedness { get; set; }
        public List<LineupPlayer> Players { get; set; } = new List<LineupPlayer>();
        public bool IsVerified { get; set; }
        public int? LineupExpectedMinutes { get; set; }
        public bool IsHomeTeam { get; set; }
        public string TeamColor { get; set; }
        public bool? IsLineupConfirmed { get; set; } 
    }
}