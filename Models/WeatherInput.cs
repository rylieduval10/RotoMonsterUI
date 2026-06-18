using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class WeatherInput
    {
        public double AvgTemp { get; set; }
        public double AvgHumidity { get; set; }
        public double WindFieldDegrees { get; set; }
        public double WindSpeed { get; set; }
        public double RainChance { get; set; }
        public int RainHours { get; set; }
        public string StadiumType { get; set; } // indoor, outdoor, retractable
        public List<int> HourlyRainChance { get; set; }
        public string ChanceOfPostponement { get; set; } // kept for backwards compatibility


        // Impact factors
        public string WindFactor { get; set; } // none, low, medium, high
        public string PostponementFactor { get; set; } // none, low, medium, high, confirmed
        public string DomeFactor { get; set; } // none, low, medium, high, confirmed (retractable only)
    }
}