using System.Text;
using HtmlTags;

namespace RotoMonsterUI
{
    public class PollOptionPlayerPicker
    {
        private readonly PollOptionPlayerPickerInput _input;

        public PollOptionPlayerPicker(PollOptionPlayerPickerInput input)
        {
            _input = input;
        }

        private string EscapeJson(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private string SerializeAliasesJson(System.Collections.Generic.List<string> aliases)
        {
            if (aliases == null || aliases.Count == 0) return "[]";

            var sb = new StringBuilder("[");
            var wrote = 0;
            for (int i = 0; i < aliases.Count; i++)
            {
                if (string.IsNullOrEmpty(aliases[i])) continue;
                if (wrote > 0) sb.Append(",");
                sb.Append($"\"{EscapeJson(aliases[i])}\"");
                wrote++;
            }
            sb.Append("]");
            return sb.ToString();
        }

        private string SerializePlayersJson()
        {
            var sb = new StringBuilder("[");
            for (int i = 0; i < _input.AvailablePlayers.Count; i++)
            {
                var p = _input.AvailablePlayers[i];
                if (i > 0) sb.Append(",");
                var posText = p.Positions != null && p.Positions.Count > 0
                    ? string.Join("/", p.Positions.ConvertAll(x => x.Abbreviation))
                    : "";
                sb.Append("{");
                sb.Append($"\"id\":{p.PlayerId},");
                sb.Append($"\"name\":\"{EscapeJson(p.PlayerName)}\",");
                sb.Append($"\"team\":\"{EscapeJson(p.TeamCode)}\",");
                sb.Append($"\"pos\":\"{EscapeJson(posText)}\",");
                sb.Append($"\"aliases\":{SerializeAliasesJson(p.Aliases)}");
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("poll-player-picker").Attr("id", _input.Id);

            if (_input.AddedPlayers.Count > 0)
            {
                var list = new HtmlTag("div").AddClass("poll-player-picker-list");
                foreach (var player in _input.AddedPlayers)
                {
                    var chip = new HtmlTag("div").AddClass("poll-player-picker-chip");
                    chip.AppendHtml(new DisplayPlayer(player).Render());

                    if (_input.CanEdit)
                    {
                        var removeBtn = new HtmlTag("button")
                        .AddClass("poll-player-picker-remove")
                        .Attr("type", "submit")
                        .Attr("name", $"{_input.Id}-remove-{player.PlayerId}")
                        .Attr("aria-label", "Remove player")
                        .AppendHtml(new Icon(new IconInput { Type = IconType.Error, Size = 14 }).Render())
                        .AppendHtml("<span class='sr-only'>Remove</span>");
                        chip.Append(removeBtn);
                    }

                    list.Append(chip);
                }
                wrapper.Append(list);
            }

            if (_input.CanEdit)
            {
                var searchRow = new HtmlTag("div").AddClass("poll-player-picker-search-row");

                var searchBox = new SearchBox()
                    .WithId($"{_input.Id}-search")
                    .WithName($"{_input.Id}-search")
                    .WithPlaceholder("Search players...")
                    .Render();

                var hiddenSelected = new HtmlTag("input")
                    .Attr("type", "hidden")
                    .Attr("id", $"{_input.Id}-selected")
                    .Attr("name", $"{_input.Id}-selected");

                var addBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("type", "submit")
                    .Attr("name", $"{_input.Id}-add")
                    .Text("Add");

                var resultsList = new HtmlTag("ul")
                    .AddClass("completionList poll-player-picker-results")
                    .Attr("id", $"{_input.Id}-results")
                    .Attr("style", "display:none;");

                searchRow.AppendHtml(searchBox);
                searchRow.Append(hiddenSelected);
                searchRow.Append(addBtn);
                searchRow.Append(resultsList);
                wrapper.Append(searchRow);

                wrapper.AppendHtml($"<script type=\"application/json\" id=\"{_input.Id}-data\">{SerializePlayersJson()}</script>");
            }

            return wrapper.ToString();
        }
    }
}