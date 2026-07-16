using System;

namespace RotoMonsterUI
{
    public class GameStateInput
    {
        public GameSport Sport { get; set; } = GameSport.Baseball;

        public bool IsGameLive { get; set; }
        public bool IsGameFinished { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public WeatherInput Weather { get; set; }

        // Baseball only
        public int CurrentOuts { get; set; }

        // Basketball only
        public int CurrentQuarter { get; set; } = 1;
        public int TotalQuarters { get; set; } = 4;
        public int QuarterLengthMinutes { get; set; } = 12;
        public double QuarterMinutesRemaining { get; set; }
        public bool IsOvertime { get; set; }
    }
}