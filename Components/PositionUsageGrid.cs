using HtmlTags;

namespace RotoMonsterUI
{
    public class PositionUsageGrid
    {
        private readonly PositionUsageColors _colors;

        public PositionUsageGrid(PositionUsageColors colors = null)
        {
            _colors = colors ?? new PositionUsageColors();
        }

        private (string Position, string Color)[] Positions => new[]
        {
            ("PG", _colors.PG),
            ("SG", _colors.SG),
            ("SF", _colors.SF),
            ("PF", _colors.PF),
            ("C",  _colors.C),
        };

        public string RenderHeader()
        {
            var row = new HtmlTag("div").AddClass("position-usage-header");
            foreach (var pos in Positions)
                row.AppendHtml($"<span class='position-usage-header-label' style='color:{pos.Color};'>{pos.Position}</span>");
            return row.ToString();
        }
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
                        .Attr("style", $"background:{Positions[i].Color};")
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