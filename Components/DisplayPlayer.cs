using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayPlayer
    {
        private DisplayPlayerInput _input;

        public DisplayPlayer(DisplayPlayerInput input)
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
            var wrapper = new HtmlTag("span").AddClass("display-player");

            var playerLink = new HtmlTag("a")
                .AddClass("display-player-name player-link")
                .Attr("href", $"/playerInfo.aspx?i={_input.PlayerId}")
                .Text(_input.PlayerName);
            wrapper.Append(playerLink);

            var team = new HtmlTag("span")
                .AddClass("ml-2 mr-1")
                .AddClass("display-player-team")
                .Text(_input.TeamCode);
            wrapper.Append(team);

            foreach (var pos in _input.Positions)
            {
                var posTag = new HtmlTag("span")
                    .AddClass("ml-1")
                    .AddClass("display-player-pos")
                    .Attr("style", $"color:{NormalizeColor(pos.Color)}")
                    .Text(pos.Abbreviation);
                wrapper.Append(posTag);
            }

            return wrapper.ToString();
        }
    }
}