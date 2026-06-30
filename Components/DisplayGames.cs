using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayGames
    {
        private List<GameInput> _games;
        private string _id;
        private bool _showSettingsLink;
        private bool _showToggle;

        public DisplayGames(List<GameInput> games, string id = "game-date-control", bool showSettingsLink = false, bool showToggle = false)
        {
            _games = games;
            _id = id;
            _showSettingsLink = showSettingsLink;
            _showToggle = showToggle;
        }

        public DisplayGames(GameInput game, string id = "game-date-control", bool showSettingsLink = false, bool showToggle = false)
        {
            _games = new List<GameInput> { game };
            _id = id;
            _showSettingsLink = showSettingsLink;
            _showToggle = showToggle;
        }

        private HtmlTag BuildGameToggle(GameInput game)
        {
            var checkboxId = $"{_id}-game-{game.GameId}";

            var label = new HtmlTag("label").AddClass("badge-checkbox").Attr("for", checkboxId);

            var checkbox = new HtmlTag("input")
            .Attr("type", "checkbox")
            .Attr("id", checkboxId)
            .Attr("name", checkboxId)
            .Attr("value", "1")
            .AddClass("game-date-toggle-checkbox");

            if (game.IsSelected)
                checkbox.Attr("checked", "checked");

            var toggle = new HtmlTag("span").AddClass("game-date-toggle modern-filter-badge");

            label.Append(checkbox);
            label.Append(toggle);

            return label;
        }

        private string BuildGameRowInner(GameInput game, bool hideTeamCells = false)
        {
            var sb = new System.Text.StringBuilder();

            sb.Append(new GameState(new GameStateInput
            {
                IsGameLive = game.IsGameLive,
                IsGameFinished = game.IsGameFinished,
                CurrentOuts = game.CurrentOuts,
                GameTimeUtc = game.GameTimeUtc,
                DisplayTimezone = game.DisplayTimezone,
                Weather = game.Weather
            }).Render());

            if (!hideTeamCells)
            {
                if (game.AwayGameTeam != null)
                    sb.Append(new GameTeam(game.AwayGameTeam).Render());
                if (game.HomeGameTeam != null)
                    sb.Append(new GameTeam(game.HomeGameTeam).Render());
            }

            if (game.Weather != null)
                sb.Append(new Weather(game.Weather).Render());

            if (!string.IsNullOrEmpty(game.ViewBoxScoreUrl))
            {
                var viewLink = new HtmlTag("a")
                    .AddClass("game-date-view")
                    .Attr("href", game.ViewBoxScoreUrl)
                    .Text("box score");
                sb.Append(viewLink.ToString());
            }

            return sb.ToString();
        }

        public string RenderSingleGameInner(bool hideTeamCells = false)
        {
            return BuildGameRowInner(_games[0], hideTeamCells);
        }

        private HtmlTag BuildGameRow(GameInput game, bool noBorder = false)
        {
            var row = new HtmlTag("div").AddClass("game-date-row");
            if (noBorder)
                row.AddClass("game-date-row--no-border");
            if (game.IsSelected)
                row.AddClass("game-date-row--selected");

            if (_showToggle)
                row.Append(BuildGameToggle(game));

            row.AppendHtml(new GameState(new GameStateInput
            {
                IsGameLive = game.IsGameLive,
                IsGameFinished = game.IsGameFinished,
                CurrentOuts = game.CurrentOuts,
                GameTimeUtc = game.GameTimeUtc,
                DisplayTimezone = game.DisplayTimezone,
                Weather = game.Weather
            }).Render());

            if (game.AwayGameTeam != null)
                row.AppendHtml(new GameTeam(game.AwayGameTeam).Render());
            if (game.HomeGameTeam != null)
                row.AppendHtml(new GameTeam(game.HomeGameTeam).Render());

            if (game.Weather != null)
                row.AppendHtml(new Weather(game.Weather).Render());

            if (!string.IsNullOrEmpty(game.ViewBoxScoreUrl))
            {
                var viewLink = new HtmlTag("a")
                    .AddClass("game-date-view")
                    .Attr("href", game.ViewBoxScoreUrl)
                    .Text("box score");
                row.Append(viewLink);
            }

            return row;
        }

        public string RenderSingleGame()
        {
            return BuildGameRow(_games[0], noBorder: true).ToString();
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("game-date-control").Attr("id", _id);

            foreach (var game in _games)
                wrapper.Append(BuildGameRow(game));

            if (_games.Count > 0 && _games[0].DisplayTimezone != null)
            {
                var tz = _games[0].DisplayTimezone;
                var tzDisplay = tz.DisplayName;
                var footer = new HtmlTag("div").AddClass("game-date-timezone");

                if (_showSettingsLink)
                    footer.AppendHtml($"Times shown in {tzDisplay} &middot; <a href='/usersettings.aspx' class='game-date-tz-link'>Change timezone</a>");
                else
                    footer.AppendHtml($"Times shown in {tzDisplay}");

                wrapper.Append(footer);
            }

            return wrapper.ToString();
        }
    }
}