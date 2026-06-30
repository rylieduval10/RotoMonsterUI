using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class GameTeamInput
    {
        public string TeamCode { get; set; }
        public float ProjectedRuns { get; set; }
        public float CurrentRuns { get; set; }
        public bool IsWinner { get; set; }
        public bool GameStarted { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public bool LineupConfirmed { get; set; }
        public List<WarningPlayer> WarningPlayers { get; set; } = new List<WarningPlayer>();
        public int? PlayerCount { get; set; }
        public string PlayerIconColor { get; set; } = "#94a3b8";
        public IconType? PlayerIconType { get; set; }
        public string BgColor { get; set; } = "FFFFFF";
        public int CurrentOuts { get; set; }
    }
}