using System;

namespace RotoMonsterUI
{
    public class WeatherInput
    {
        public double AvgTemp { get; set; }
        public double AvgHumidity { get; set; }
        public string WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double ChanceOfPostponement { get; set; }
        public double RainChance { get; set; }
        public string StadiumType { get; set; } // indoor, outdoor, retractable
    }
}