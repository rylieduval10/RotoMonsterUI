using HtmlTags;

namespace RotoMonsterUI
{
    public class LineupDotInput
    {
        public bool IsConfirmed { get; set; }
    }

    public class LineupDot
    {
        private readonly LineupDotInput _input;

        public LineupDot(LineupDotInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var dot = new HtmlTag("span")
                .AddClass(_input.IsConfirmed ? "lineup-dot lineup-dot-confirmed" : "lineup-dot lineup-dot-empty")
                .ToString();
            return new CustomTooltip(dot, _input.IsConfirmed ? "Lineup Confirmed" : "Lineup Not Confirmed").Render();
        }
    }
}