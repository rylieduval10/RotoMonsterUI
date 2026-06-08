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

        public string Render()
        {
            var wrapper = new HtmlTag("span").AddClass("display-player");

            var playerLink = new HtmlTag("a")
                .AddClass("display-player-name")
                .Attr("href", $"/playerInfo.aspx?i={_input.PlayerId}")
                .Text(_input.PlayerName);
            wrapper.Append(playerLink);

            var team = new HtmlTag("span").AddClass("display-player-team").Text($" {_input.TeamCode}");
            wrapper.Append(team);

            foreach (var pos in _input.Positions)
            {
                var posTag = new HtmlTag("span")
                    .AddClass("display-player-pos")
                    .Attr("style", $"color:{pos.Color}")
                    .Text($" {pos.Abbreviation}");
                wrapper.Append(posTag);
            }

            return wrapper.ToString();
        }
    }
}