using System;

namespace RotoMonsterUI
{
    public class GameStateInput
    {
        public bool IsGameLive { get; set; }
        public bool IsGameFinished { get; set; }
        public int CurrentOuts { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public WeatherInput Weather { get; set; }
    }
}