using System;

namespace RotoMonsterUI
{
    public class DisplayGameInput
    {
        public string AwayTeamCode { get; set; }
        public string HomeTeamCode { get; set; }
        public double AwayTeamProjectedRuns { get; set; }
        public double HomeTeamProjectedRuns { get; set; }
        public double AwayTeamCurrentRuns { get; set; }
        public double HomeTeamCurrentRuns { get; set; }
        public string CurrentInning { get; set; }
        public bool IsGameFinished { get; set; }
        public bool IsGameLive { get; set; }
        public DateTime GameTimeUtc { get; set; }
        public TimeZoneInfo DisplayTimezone { get; set; }
        public string ViewBoxScoreUrl { get; set; }
        public WeatherInput Weather { get; set; }
    }
}