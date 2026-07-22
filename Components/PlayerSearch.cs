using System.Text;
using HtmlTags;

namespace RotoMonsterUI
{
    public class PlayerSearch
    {
        private readonly PlayerSearchInput _input;

        public PlayerSearch(PlayerSearchInput input)
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
            var wrapper = new HtmlTag("div").AddClass("player-search").Attr("id", _input.Id);

            if (!string.IsNullOrEmpty(_input.UrlFormat))
                wrapper.Attr("data-urlformat", _input.UrlFormat);

            wrapper.Attr("data-maxresults", _input.MaxResults.ToString());

            var searchBox = new SearchBox()
                .WithId($"{_input.Id}-search")
                .WithName($"{_input.Id}-search")
                .WithPlaceholder(_input.Placeholder)
                .Render();
            wrapper.AppendHtml(searchBox);

            var hiddenSelected = new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("id", $"{_input.Id}-selected")
                .Attr("name", $"{_input.Id}-selected");
            wrapper.Append(hiddenSelected);

            var resultsList = new HtmlTag("ul")
                .AddClass("completionList player-search-results")
                .Attr("id", $"{_input.Id}-results")
                .Attr("style", "display:none;");
            wrapper.Append(resultsList);

            wrapper.AppendHtml($"<script type=\"application/json\" id=\"{_input.Id}-data\">{SerializePlayersJson()}</script>");

            return wrapper.ToString();
        }
    }
}