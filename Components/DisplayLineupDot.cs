using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayLineupDotInput
    {
        public bool IsConfirmed { get; set; }
    }

    public class DisplayLineupDot
    {
        private readonly DisplayLineupDotInput _input;

        public DisplayLineupDot(DisplayLineupDotInput input)
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