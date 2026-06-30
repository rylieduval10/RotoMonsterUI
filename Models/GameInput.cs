using System;

namespace RotoMonsterUI
{
    public class GameInput
    {
        public int GameId { get; set; }
        public bool IsSelected { get; set; } = false;

        public GameTeamInput AwayGameTeam { get; set; }
        public GameTeamInput HomeGameTeam { get; set; }

        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public string ViewBoxScoreUrl { get; set; }
        public WeatherInput Weather { get; set; }
        public int CurrentOuts { get; set; }
        public int CurrentInning { get; set; }
    }
}