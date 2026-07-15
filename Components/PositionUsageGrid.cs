using HtmlTags;

namespace RotoMonsterUI
{
    public class PositionUsageGrid
    {
        private readonly PositionUsageColors _colors;
        private readonly bool _showTooltips;

        public PositionUsageGrid(PositionUsageColors colors = null, bool showTooltips = true)
        {
            _colors = colors ?? new PositionUsageColors();
            _showTooltips = showTooltips;
        }

        private (string Abbreviation, string FullName, string BgColor, string TextColor)[] Positions => new[]
        {
            ("PG", "Point guard",    _colors.PG, _colors.PGText),
            ("SG", "Shooting guard", _colors.SG, _colors.SGText),
            ("SF", "Small forward",  _colors.SF, _colors.SFText),
            ("PF", "Power forward",  _colors.PF, _colors.PFText),
            ("C",  "Center",         _colors.C,  _colors.CText),
        };

        public string RenderHeader()
        {
            var row = new HtmlTag("div").AddClass("position-usage-header");
            foreach (var pos in Positions)
                row.AppendHtml($"<span class='position-usage-header-label' style='color:{pos.BgColor};'>{pos.Abbreviation}</span>");
            return row.ToString();
        }

        public string Render(PositionUsageInput input, int rowId = 0)
        {
            var values = new int?[] { input.PG, input.SG, input.SF, input.PF, input.C };
            var row = new HtmlTag("div").AddClass("position-usage-row");

            for (int i = 0; i < Positions.Length; i++)
            {
                var value = values[i];
                var pos = Positions[i];

                if (!value.HasValue)
                {
                    row.AppendHtml("<span class='position-usage-empty'></span>");
                    continue;
                }

                HtmlTag pill;
                if (value.Value == 0)
                {
                    pill = new HtmlTag("span")
                        .AddClass("position-usage-pill position-usage-pill--outline")
                        .Attr("style", $"border-color:{pos.BgColor}; color:{pos.BgColor};");
                }
                else
                {
                    pill = new HtmlTag("span")
                        .AddClass("position-usage-pill")
                        .Attr("style", $"background:{pos.BgColor}; color:{pos.TextColor};")
                        .Text(value.Value.ToString());
                }

                if (_showTooltips)
                {
                    var tooltipId = $"position-usage-tooltip-{rowId}-{pos.Abbreviation}";
                    pill.AddClass("bm-tooltip-trigger").Attr("data-bm-tooltip", tooltipId);

                    var wrap = new HtmlTag("span").AddClass("bm-tooltip-wrap");
                    wrap.Append(pill);
                    wrap.Append(new HtmlTag("div").AddClass("bm-tooltip-content bm-tooltip-content--centered").Attr("id", tooltipId).Text(pos.FullName));
                    row.Append(wrap);
                }
                else
                {
                    row.Append(pill);
                }
            }

            return row.ToString();
        }
    }
}