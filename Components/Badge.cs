using HtmlTags;

namespace RotoMonsterUI
{
    public class Badge
    {
        private BadgeInput _input;

        public Badge(BadgeInput input)
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
            var badge = new HtmlTag("span").AddClass("badge-pill-custom");

            if (!string.IsNullOrEmpty(_input.ColorClass))
                badge.AddClass(_input.ColorClass);

            if (!string.IsNullOrEmpty(_input.Color))
                badge.Attr("style", $"background-color:{NormalizeColor(_input.Color)}");

            if (!string.IsNullOrEmpty(_input.TooltipText))
            {
                badge
                    .Attr("data-toggle", "tooltip")
                    .Attr("data-placement", "top")
                    .Attr("title", _input.TooltipText);
            }

            badge.AppendHtml(_input.BadgeText);

            return badge.ToString();
        }
    }
}