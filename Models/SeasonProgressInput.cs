namespace RotoMonsterUI
{
    public class SeasonProgressInput
    {
        public double SeasonPercent { get; set; }
        public int? DaysUntilSeason { get; set; }

        public double? PlayoffPercent { get; set; }

        public double? UnusedPercent { get; set; }

        public string Label { get; set; }

        public bool ShowTooltip { get; set; } = true;
    }
}