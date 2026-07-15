using HtmlTags;

namespace RotoMonsterUI
{
    public class PositionUsageGrid
    {
        // Same color always maps to the same position, across every row - lets you scan down a
        // column and instantly recognize "that's the PF column" regardless of which player's row.
        private static readonly (string Position, string BgColor, string TextColor)[] Positions = new[]
        {
            ("PG", "#D3D1C7", "#444441"),
            ("SG", "#FAC775", "#633806"),
            ("SF", "#F09595", "#501313"),
            ("PF", "#7F77DD", "#26215C"),
            ("C",  "#9FE1CB", "#04342C"),
        };

        // Render once, above the rows that use this grid.
        public static string RenderHeader()
        {
            var row = new HtmlTag("div").AddClass("position-usage-header");
            foreach (var pos in Positions)
                row.AppendHtml($"<span class='position-usage-header-label'>{pos.Position}</span>");
            return row.ToString();
        }

        // Render once per player row - meant to slot into an existing table cell, not stand alone.
        public string Render(PositionUsageInput input)
        {
            var values = new int?[] { input.PG, input.SG, input.SF, input.PF, input.C };
            var row = new HtmlTag("div").AddClass("position-usage-row");

            for (int i = 0; i < Positions.Length; i++)
            {
                var value = values[i];
                if (value.HasValue)
                {
                    var pill = new HtmlTag("span")
                        .AddClass("position-usage-pill")
                        .Attr("style", $"background:{Positions[i].BgColor}; color:{Positions[i].TextColor};")
                        .Text(value.Value.ToString());
                    row.Append(pill);
                }
                else
                {
                    row.AppendHtml("<span class='position-usage-empty'></span>");
                }
            }

            return row.ToString();
        }
    }
}