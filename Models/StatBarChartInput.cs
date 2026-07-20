using System.Collections.Generic;

namespace RotoMonsterUI
{
    // Bar/column chart of raw per-game stat values (blocks, steals, etc.).
    // x = game date, y = the stat that game. Usually one series; pass more to
    // group columns per game (e.g. steals + blocks side by side).
    public class StatBarChartInput
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string XAxisLabel { get; set; } = "Game";
        public string YAxisLabel { get; set; }
        public List<ChartSeries> Series { get; set; } = new List<ChartSeries>();
        public int Height { get; set; } = 320;
    }
}