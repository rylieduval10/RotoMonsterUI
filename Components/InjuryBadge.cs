using HtmlTags;

namespace RotoMonsterUI
{
    public class InjuryBadge
    {
        private InjuryBadgeInput _input;

        public InjuryBadge(InjuryBadgeInput input)
        {
            _input = input;
        }

        private string NormalizeColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return color;
            return color.StartsWith("#") ? color : "#" + color;
        }

        public string Render()
        {
            var badge = new HtmlTag("span")
                .AddClass("injury-badge")
                .Attr("style", $"background-color:{NormalizeColor(_input.Color)}")
                .AppendHtml(_input.BadgeText);

            if (!string.IsNullOrEmpty(_input.TooltipText))
            {
                badge
                    .Attr("data-toggle", "tooltip")
                    .Attr("data-placement", "top")
                    .Attr("title", _input.TooltipText);
            }

            return badge.ToString();
        }
    }
}