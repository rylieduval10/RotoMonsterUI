using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DisplayGameInput
    {
        public int GameId { get; set; }
        public bool IsSelected { get; set; } = false;

        public string AwayTeamCode { get; set; }
        public string HomeTeamCode { get; set; }
        public float AwayTeamProjectedRuns { get; set; }
        public float HomeTeamProjectedRuns { get; set; }
        public float AwayTeamCurrentRuns { get; set; }
        public float HomeTeamCurrentRuns { get; set; }
        public int CurrentInning { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public string ViewBoxScoreUrl { get; set; }
        public WeatherInput Weather { get; set; }

        public bool AwayTeamLineupConfirmed { get; set; }
        public bool HomeTeamLineupConfirmed { get; set; }
        public int CurrentOuts { get; set; }

        public int? AwayTeamPlayerCount { get; set; }
        public int? HomeTeamPlayerCount { get; set; }

        public IconType? AwayTeamPlayerIconType { get; set; }
        public string AwayTeamPlayerIconColor { get; set; } = "#94a3b8";
        public IconType? HomeTeamPlayerIconType { get; set; }
        public string HomeTeamPlayerIconColor { get; set; } = "#94a3b8";

        public List<WarningPlayer> AwayTeamWarningPlayers { get; set; } = new List<WarningPlayer>();
        public List<WarningPlayer> HomeTeamWarningPlayers { get; set; } = new List<WarningPlayer>();
        public PlayerWarningType? WarningPlayersType { get; set; }
    }
}
