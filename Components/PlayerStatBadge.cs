using HtmlTags;

namespace RotoMonsterUI
{
    public class PlayerStatBadge
    {
        private readonly PlayerStatBadgeInput _input;

        public PlayerStatBadge(PlayerStatBadgeInput input)
        {
            _input = input;
        }

        private string NormalizeColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return color;
            if (color.StartsWith("var(") || color.StartsWith("#")) return color;
            return "#" + color;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("player-stat-badge");

            foreach (var stat in _input.Stats)
            {
                var item = new HtmlTag("div").AddClass("player-stat-badge-item");
                var styleParts = new System.Collections.Generic.List<string>();
                if (!string.IsNullOrEmpty(stat.BackgroundColor))
                    styleParts.Add($"background:{NormalizeColor(stat.BackgroundColor)}");
                if (!string.IsNullOrEmpty(stat.BorderColor))
                    styleParts.Add($"border-color:{NormalizeColor(stat.BorderColor)}; border-width:2px");
                if (styleParts.Count > 0)
                    item.Attr("style", string.Join("; ", styleParts) + ";");

                var label = new HtmlTag("span").AddClass("player-stat-badge-label").Text(stat.Label);
                var value = new HtmlTag("span").AddClass("player-stat-badge-value").AppendHtml(stat.Value);

                item.Append(label);
                item.Append(value);
                wrapper.Append(item);
            }

            return wrapper.ToString();
        }
    }
}