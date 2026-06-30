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

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("player-stat-badge");

            foreach (var stat in _input.Stats)
            {
                var item = new HtmlTag("div").AddClass("player-stat-badge-item");

                var label = new HtmlTag("span").AddClass("player-stat-badge-label").Text(stat.Label);
                var value = new HtmlTag("span").AddClass("player-stat-badge-value").Text(stat.Value);

                item.Append(label);
                item.Append(value);
                wrapper.Append(item);
            }

            return wrapper.ToString();
        }
    }
}