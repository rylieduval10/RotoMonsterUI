using HtmlTags;

namespace RotoMonsterUI
{
    public class InjuryBadgeInput
    {
        public string StatusAbbreviation { get; set; }
        public string StatusText { get; set; }
        public string StatusColor { get; set; }
        public int NumberOfGames { get; set; }
        public string StatusDetails { get; set; }
    }

    public class InjuryBadge
    {
        private readonly InjuryBadgeInput _input;

        public InjuryBadge(InjuryBadgeInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var badgeText = _input.NumberOfGames > 0
                ? $"{_input.StatusAbbreviation} {_input.NumberOfGames}g"
                : _input.StatusAbbreviation;

            var tooltipText = !string.IsNullOrEmpty(_input.StatusDetails)
                ? $"{_input.StatusText} – {_input.StatusDetails}"
                : _input.StatusText;

            var color = string.IsNullOrEmpty(_input.StatusColor) ? "e05c00" : _input.StatusColor;
            var normalizedColor = color.StartsWith("#") ? color : "#" + color;

            var badge = new HtmlTag("span")
                .AddClass("injury-badge")
                .Attr("style", $"background-color:{normalizedColor};")
                .Text(badgeText);

            return new CustomTooltip(badge.ToString(), tooltipText).Render();
        }
    }
}