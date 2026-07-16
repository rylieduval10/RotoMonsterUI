using System;

namespace RotoMonsterUI
{
    public enum GameSport
    {
        Baseball,
        Basketball
    }

    public class GameInput
    {
        public int GameId { get; set; }
        public bool IsSelected { get; set; } = false;

        public GameSport Sport { get; set; } = GameSport.Baseball;

        public GameTeamInput AwayGameTeam { get; set; }
        public GameTeamInput HomeGameTeam { get; set; }

        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public string ViewBoxScoreUrl { get; set; }
        public WeatherInput Weather { get; set; }

        // Baseball only
        public int CurrentOuts { get; set; }
        public int CurrentInning { get; set; }

        // Basketball only
        public int CurrentQuarter { get; set; } = 1;
        public int TotalQuarters { get; set; } = 4;
        public int QuarterLengthMinutes { get; set; } = 12;
        public double QuarterMinutesRemaining { get; set; }
        public bool IsOvertime { get; set; }
    }
}