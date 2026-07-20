using System.Collections.Generic;

namespace RotoMonsterUI
{
    // A single point on a chart: X is the category label (week number, game
    // date, etc.), Y is the value.
    public class ChartPoint
    {
        public string X { get; set; }
        public double Y { get; set; }
    }

    // One line (or column group) on a chart. Series sharing the same X labels
    // line up as multiple lines / grouped columns.
    public class ChartSeries
    {
        public string Name { get; set; }
        public List<ChartPoint> Points { get; set; } = new List<ChartPoint>();
    }
}