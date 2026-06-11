using System;

namespace RotoMonsterUI
{
    public class DisplayGameInput
    {
        public string AwayTeamCode { get; set; }
        public string HomeTeamCode { get; set; }
        public float AwayTeamProjectedRuns { get; set; }
        public float HomeTeamProjectedRuns { get; set; }
        public float AwayTeamCurrentRuns { get; set; }
        public float HomeTeamCurrentRuns { get; set; }
        public string CurrentInning { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public string ViewBoxScoreUrl { get; set; }
        public WeatherInput Weather { get; set; }
    }
}