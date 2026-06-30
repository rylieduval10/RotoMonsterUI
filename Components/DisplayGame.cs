using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayGame
    {
        private readonly GameInput _input;
        private readonly bool _hideTeamCells;
        private readonly bool _noBorder;
        private readonly bool _showToggle;
        private readonly string _toggleId;

        public DisplayGame(GameInput input, bool hideTeamCells = false, bool noBorder = false, bool showToggle = false, string toggleId = null)
        {
            _input = input;
            _hideTeamCells = hideTeamCells;
            _noBorder = noBorder;
            _showToggle = showToggle;
            _toggleId = toggleId;
        }

        private HtmlTag BuildToggle()
        {
            var checkboxId = _toggleId ?? $"game-toggle-{_input.GameId}";

            var label = new HtmlTag("label").AddClass("badge-checkbox").Attr("for", checkboxId);

            var checkbox = new HtmlTag("input")
            .Attr("type", "checkbox")
            .Attr("id", checkboxId)
            .Attr("name", checkboxId)
            .Attr("value", "1")
            .AddClass("game-date-toggle-checkbox");

            if (_input.IsSelected)
                checkbox.Attr("checked", "checked");

            var toggle = new HtmlTag("span").AddClass("game-date-toggle modern-filter-badge");

            label.Append(checkbox);
            label.Append(toggle);

            return label;
        }

        public string RenderInner()
        {
            var sb = new System.Text.StringBuilder();

            sb.Append(new GameState(new GameStateInput
            {
                IsGameLive = _input.IsGameLive,
                IsGameFinished = _input.IsGameFinished,
                CurrentOuts = _input.CurrentOuts,
                GameTimeUtc = _input.GameTimeUtc,
                DisplayTimezone = _input.DisplayTimezone,
                Weather = _input.Weather
            }).Render());

            if (!_hideTeamCells)
            {
                if (_input.AwayGameTeam != null)
                    sb.Append(new GameTeam(_input.AwayGameTeam).Render());
                if (_input.HomeGameTeam != null)
                    sb.Append(new GameTeam(_input.HomeGameTeam).Render());
            }

            if (_input.Weather != null)
                sb.Append(new Weather(_input.Weather).Render());

            if (!string.IsNullOrEmpty(_input.ViewBoxScoreUrl))
            {
                var viewLink = new HtmlTag("a")
                    .AddClass("game-date-view")
                    .Attr("href", _input.ViewBoxScoreUrl)
                    .Text("box score");
                sb.Append(viewLink.ToString());
            }

            return sb.ToString();
        }

        public string Render()
        {
            var row = new HtmlTag("div").AddClass("game-date-row");
            if (_noBorder)
                row.AddClass("game-date-row--no-border");
            if (_input.IsSelected)
                row.AddClass("game-date-row--selected");

            if (_showToggle)
                row.Append(BuildToggle());

            row.AppendHtml(RenderInner());

            return row.ToString();
        }
    }
}