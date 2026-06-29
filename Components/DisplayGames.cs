using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayGames
    {
        private List<DisplayGameInput> _games;
        private string _id;
        private bool _showSettingsLink;
        private bool _showToggle;

        public DisplayGames(List<DisplayGameInput> games, string id = "game-date-control", bool showSettingsLink = false, bool showToggle = false)
        {
            _games = games;
            _id = id;
            _showSettingsLink = showSettingsLink;
            _showToggle = showToggle;
        }

        public DisplayGames(DisplayGameInput game, string id = "game-date-control", bool showSettingsLink = false, bool showToggle = false)
        {
            _games = new List<DisplayGameInput> { game };
            _id = id;
            _showSettingsLink = showSettingsLink;
            _showToggle = showToggle;
        }

        private int GetInningPercent(int inning)
        {
            if (inning <= 0) return 0;
            if (inning == 9) return 90;
            if (inning > 9) return 100;
            return inning * 10;
        }

        private HtmlTag BuildGameState(DisplayGameInput game)
        {
            var wrapper = new HtmlTag("div").AddClass("game-state-wrapper");

            if (game.IsGameLive && !game.IsGameFinished)
            {
                var progressPercent = Math.Min(100, game.CurrentOuts / 54.0 * 100);
                wrapper.Attr("style", $"background: linear-gradient(to right, rgba(128,128,128,0.35) {progressPercent:0.0}%, transparent {progressPercent:0.0}%); border-radius: 6px; padding: 3px 6px;");
            }

            if (game.IsGameFinished)
            {
                var final = new HtmlTag("div").AddClass("game-state-final");
                final.Text("Final");
                wrapper.Append(final);
            }
            else if (game.IsGameLive)
            {
                int inning = game.CurrentOuts / 6 + 1;
                int halfInningPos = game.CurrentOuts % 6;
                bool isBottom = halfInningPos >= 3;
                int topOuts = isBottom ? 3 : halfInningPos;
                int bottomOuts = isBottom ? halfInningPos - 3 : 0;
                string inningLabel = OrdinalHelper.GetOrdinal(inning);

                var outsWrapper = new HtmlTag("div").AddClass("game-state-outs");

                var topHalf = new HtmlTag("div").AddClass("game-state-outs-half");
                for (int i = 0; i < 3; i++)
                {
                    var circle = new HtmlTag("span").AddClass(i < topOuts ? "out-circle out-circle--filled" : "out-circle");
                    topHalf.Append(circle);
                }

                var inningTag = new HtmlTag("span").AddClass("game-state-inning-label").Text(inningLabel);

                var bottomHalf = new HtmlTag("div").AddClass("game-state-outs-half");
                for (int i = 0; i < 3; i++)
                {
                    var circle = new HtmlTag("span").AddClass(i < bottomOuts ? "out-circle out-circle--filled" : "out-circle");
                    bottomHalf.Append(circle);
                }

                outsWrapper.Append(topHalf);
                outsWrapper.Append(inningTag);
                outsWrapper.Append(bottomHalf);
                wrapper.Append(outsWrapper);
            }
            else
            {
                var localTime = TimeZoneInfo.ConvertTimeFromUtc(game.GameTimeUtc, game.DisplayTimezone);
                var timeStr = localTime.ToString("h:mmtt").ToLower();
                var until = game.GameTimeUtc - DateTime.UtcNow;
                var untilStr = until.TotalHours >= 1
                    ? $"in {Math.Round(until.TotalHours, 1)}h"
                    : $"in {until.Minutes}m";

                var upcoming = new HtmlTag("div").AddClass("game-state-upcoming");

                if (until.TotalHours <= 4 && until.TotalHours >= 0)
                {
                    var yellowPercent = (float)(1.0 - until.TotalHours / 4.0);
                    var bgColor = ColorHelper.GetYellowColorCode(yellowPercent * 100f, 0f, 100f, true);
                    upcoming.Attr("style", $"background-color:#{bgColor}; border: 1px solid #d1d5db; text-align:center; border-radius:4px; padding: 0.25rem 0.5rem;");
                }
                else
                {
                    upcoming.Attr("style", "border: 1px solid #d1d5db; text-align:center; border-radius:4px; padding: 0.25rem 0.5rem; color: inherit;");
                }

                upcoming.Text($"{timeStr} {untilStr}");

                if (game.Weather != null)
                {
                    var postponeColor = GetPostponementColor(game.Weather?.PostponementFactor);
                    if (postponeColor != null)
                    {
                        var postponeIcon = new Icon(new IconInput { Type = IconType.PostponementChanceWarning, Color = postponeColor, Fill = postponeColor, Size = 18 }).Render();
                        var postponeLabel = string.IsNullOrEmpty(game.Weather.PostponementFactor) ? ""
                            : char.ToUpper(game.Weather.PostponementFactor[0]) + game.Weather.PostponementFactor.Substring(1).ToLower();
                        upcoming.AppendHtml("&nbsp;");
                        var postponeTooltip = !string.IsNullOrEmpty(game.Weather.PostponementReason)
                            ? $"Postponement: {postponeLabel} — {game.Weather.PostponementReason}"
                            : $"Postponement: {postponeLabel}";
                        upcoming.AppendHtml(new CustomTooltip(postponeIcon, postponeTooltip).Render());
                    }
                }

                wrapper.Append(upcoming);
            }

            return wrapper;
        }

        private float GetRuns(float projectedRuns, float currentRuns, bool gameStarted)
        {
            return gameStarted ? currentRuns : projectedRuns;
        }

        private HtmlTag BuildTeamCell(string teamCode, float runs, string bgColor, bool isWinner, bool gameStarted, bool isGameFinished, bool lineupConfirmed, List<WarningPlayer> warningPlayers, PlayerWarningType? warningType)
        {
            var cell = new HtmlTag("div").AddClass("game-team-cell");
            if (isWinner && isGameFinished)
                cell.AddClass("winner");
            cell.Attr("style", $"background-color:#{bgColor};");

            if (!gameStarted)
            {
                cell.AppendHtml(new DisplayLineupDot(new DisplayLineupDotInput { IsConfirmed = lineupConfirmed }).Render());

                if (lineupConfirmed && warningPlayers != null && warningType.HasValue)
                {
                    cell.AppendHtml(new DisplayWarningIcon(new DisplayWarningIconInput
                    {
                        TeamCode = teamCode,
                        WarningPlayers = warningPlayers,
                        WarningType = warningType.Value
                    }).Render());
                }
            }

            cell.Append(new HtmlTag("span").AddClass("game-team-code").Text(teamCode));

            if (gameStarted)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(runs.ToString("0")));
            else if (runs != 0)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(runs.ToString("0.0")));

            return cell;
        }

        private string GetPostponementColor(string chance)
        {
            if (string.IsNullOrEmpty(chance)) return null;
            switch (chance.ToLower())
            {
                case "medium": return "#FBBF24";
                case "high": return "#FB7185";
                case "confirmed": return "#dc2626";
                default: return null;
            }
        }

        private HtmlTag BuildGameToggle(DisplayGameInput game)
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

        private string BuildGameRowInner(DisplayGameInput game, bool hideTeamCells = false)
        {
            bool gameStarted = game.IsGameLive || game.IsGameFinished;

            if (!game.IsGameFinished && game.IsGameLive)
            {
                bool homeWinning = game.HomeTeamCurrentRuns > game.AwayTeamCurrentRuns;
                bool tied = game.HomeTeamCurrentRuns == game.AwayTeamCurrentRuns;

                if (game.CurrentOuts >= 54 && !tied)
                    game.IsGameFinished = true;
                else if (game.CurrentOuts >= 51 && homeWinning)
                    game.IsGameFinished = true;
            }

            float awayRuns = GetRuns(game.AwayTeamProjectedRuns, game.AwayTeamCurrentRuns, gameStarted);
            float homeRuns = GetRuns(game.HomeTeamProjectedRuns, game.HomeTeamCurrentRuns, gameStarted);

            string awayColor, homeColor;
            bool awayWinner = false, homeWinner = false;

            if (!gameStarted)
            {
                awayColor = ColorHelper.GetYellowColorCode(awayRuns, 3.5f, 6.5f, true);
                homeColor = ColorHelper.GetYellowColorCode(homeRuns, 3.5f, 6.5f, true);
            }
            else
            {
                // Track winner separately for the winner CSS class
                if (awayRuns > homeRuns) awayWinner = true;
                else if (homeRuns > awayRuns) homeWinner = true;

                // Color based on runs vs league average, scaled to game progress
                float avgRuns = 4.5f * (float)Math.Min(1, game.CurrentOuts / 54.0);

                if (awayRuns >= avgRuns)
                    awayColor = ColorHelper.GetGreenColorCode(awayRuns - avgRuns, 0f, avgRuns, true);
                else
                    awayColor = ColorHelper.GetRedColorCode(avgRuns - awayRuns, 0f, avgRuns, true);

                if (homeRuns >= avgRuns)
                    homeColor = ColorHelper.GetGreenColorCode(homeRuns - avgRuns, 0f, avgRuns, true);
                else
                    homeColor = ColorHelper.GetRedColorCode(avgRuns - homeRuns, 0f, avgRuns, true);
            }
            var sb = new System.Text.StringBuilder();
            sb.Append(BuildGameState(game).ToString());
            if (!hideTeamCells)
            {
                sb.Append(BuildTeamCell(game.AwayTeamCode, awayRuns, awayColor, awayWinner, gameStarted, game.IsGameFinished, game.AwayTeamLineupConfirmed, game.WarningPlayers, game.WarningPlayersType).ToString());
                sb.Append(BuildTeamCell(game.HomeTeamCode, homeRuns, homeColor, homeWinner, gameStarted, game.IsGameFinished, game.HomeTeamLineupConfirmed, game.WarningPlayers, game.WarningPlayersType).ToString());
            }

            if (game.Weather != null)
            {
                var weather = new HtmlTag("div").AddClass("game-date-weather");
                bool isIndoor = game.Weather.StadiumType?.ToLower() == "d";
                bool domeHighOrConfirmed = !string.IsNullOrEmpty(game.Weather.DomeFactor) &&
                    (game.Weather.DomeFactor.ToLower() == "high" || game.Weather.DomeFactor.ToLower() == "confirmed");
                bool skipWeatherIcon = isIndoor || domeHighOrConfirmed || game.Weather.AvgTemp == 0;

                if (!skipWeatherIcon)
                {
                    var rainHoursText = game.Weather.RainHours > 0 ? $" for {game.Weather.RainHours}h" : "";
                    var rainBars = game.Weather.RainChance >= 10 && game.Weather.HourlyRainChance != null && game.Weather.HourlyRainChance.Count > 0
                        ? BuildRainBars(game.Weather.RainChance, game.Weather.HourlyRainChance, false)
                        : "";
                    var tooltipContent = $"{game.Weather.AvgTemp}° · H{game.Weather.AvgHumidity}% · Rain {game.Weather.RainChance}%{rainHoursText} {rainBars}";
                    var weatherIcon = new Icon(new IconInput { Type = IconType.Weather, Size = 16, Color = "#378ADD" }).Render();
                    weather.AppendHtml(new CustomTooltip(weatherIcon, tooltipContent).Render());
                }

                string windColor = null;
                string windStroke = null;

                if (!isIndoor)
                {
                    switch (game.Weather.WindFactor?.ToLower())
                    {
                        case "low": windColor = "#888780"; windStroke = "#5F5E5A"; break;
                        case "medium": windColor = "#F59E0B"; windStroke = "#D97706"; break;
                        case "high": windColor = "#E24B4A"; windStroke = "#A32D2D"; break;
                    }

                    if (windColor != null)
                    {
                        var windArrow = new FieldWindArrow((int)game.Weather.WindFieldDegrees)
                            .WithSize(28)
                            .WithColor(windColor)
                            .WithStrokeColor(windStroke)
                            .Render();
                        if (!skipWeatherIcon) weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                        var windTooltipText = !string.IsNullOrEmpty(game.Weather.WindField)
                            ? $"{game.Weather.WindSpeed}mph {game.Weather.WindField}"
                            : $"{game.Weather.WindSpeed}mph";
                        weather.AppendHtml(new CustomTooltip(windArrow, windTooltipText).WithCentered().Render());
                    }
                }

                if (isIndoor)
                {
                    var domeIcon = new Icon(new IconInput { Type = IconType.Dome, Color = "#FB7185", Fill = "#FB718526", Size = 20 }).Render();
                    weather.AppendHtml(new CustomTooltip(domeIcon, "Stadium is a dome.").Render());
                }
                else if (!string.IsNullOrEmpty(game.Weather.DomeFactor) && game.Weather.DomeFactor.ToLower() != "none")
                {
                    string domeColor;
                    string domeTooltip;
                    switch (game.Weather.DomeFactor.ToLower())
                    {
                        case "low": domeColor = "#888780"; domeTooltip = "Stadium is expected to be open."; break;
                        case "medium": domeColor = "#F59E0B"; domeTooltip = "Stadium may be closed."; break;
                        case "high": domeColor = "#FB7185"; domeTooltip = "Stadium is expected to be closed."; break;
                        case "confirmed": domeColor = "#FB7185"; domeTooltip = "Stadium will be closed."; break;
                        default: domeColor = null; domeTooltip = null; break;
                    }

                    if (domeColor != null)
                    {
                        var isConfirmed = game.Weather.DomeFactor.ToLower() == "confirmed";
                        var domeIcon = new Icon(new IconInput
                        {
                            Type = isConfirmed ? IconType.Dome : IconType.RetractableDome,
                            Color = domeColor,
                            Fill = isConfirmed ? domeColor + "26" : "none",
                            Size = isConfirmed ? 20 : 24
                        }).Render();

                        if (!skipWeatherIcon || windColor != null)
                            weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));

                        weather.AppendHtml(new CustomTooltip(domeIcon, domeTooltip).Render());
                    }
                }

                sb.Append(weather.ToString());
            }

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

        private string BuildRainBars(double rainChance, List<int> hourlyRainChance, bool whiteMode)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("<svg width=\"20\" height=\"16\" viewBox=\"0 0 20 16\" xmlns=\"http://www.w3.org/2000/svg\">");

            int barCount = 5;
            int barWidth = 2;
            int barGap = 2;
            int maxHeight = 14;

            for (int i = 0; i < barCount; i++)
            {
                float barValue;
                if (hourlyRainChance != null && hourlyRainChance.Count > i)
                    barValue = hourlyRainChance[i];
                else
                    barValue = (float)(rainChance * ((i + 1.0) / barCount));

                string color = whiteMode
                    ? $"rgba(255,255,255,{0.4 + (barValue / 100.0) * 0.6:0.00})"
                    : "#" + ColorHelper.GetBlueColorCode(barValue, 0f, 100f, true);

                int barHeight = Math.Max(2, (int)(maxHeight * (barValue / 100.0)));
                int x = i * (barWidth + barGap);
                int y = maxHeight - barHeight;
                sb.Append($"<rect x=\"{x}\" y=\"{y}\" width=\"{barWidth}\" height=\"{barHeight}\" fill=\"{color}\" stroke=\"#4a90d9\" stroke-width=\"0.5\" rx=\"1\"/>");
            }

            sb.Append("</svg>");
            return sb.ToString();
        }

        private HtmlTag BuildGameRow(DisplayGameInput game, bool noBorder = false)
        {
            bool gameStarted = game.IsGameLive || game.IsGameFinished;

            if (!game.IsGameFinished && game.IsGameLive)
            {
                bool homeWinning = game.HomeTeamCurrentRuns > game.AwayTeamCurrentRuns;
                bool tied = game.HomeTeamCurrentRuns == game.AwayTeamCurrentRuns;

                if (game.CurrentOuts >= 54 && !tied)
                    game.IsGameFinished = true;
                else if (game.CurrentOuts >= 51 && homeWinning)
                    game.IsGameFinished = true;
            }

            var row = new HtmlTag("div").AddClass("game-date-row");
            if (noBorder)
                row.AddClass("game-date-row--no-border");
            if (game.IsSelected)
                row.AddClass("game-date-row--selected");

            // Toggle
            if (_showToggle)
                row.Append(BuildGameToggle(game));

            row.Append(BuildGameState(game));

            float awayRuns = GetRuns(game.AwayTeamProjectedRuns, game.AwayTeamCurrentRuns, gameStarted);
            float homeRuns = GetRuns(game.HomeTeamProjectedRuns, game.HomeTeamCurrentRuns, gameStarted);

            string awayColor, homeColor;
            bool awayWinner = false, homeWinner = false;

            if (!gameStarted)
            {
                awayColor = ColorHelper.GetYellowColorCode(awayRuns, 3.5f, 6.5f, true);
                homeColor = ColorHelper.GetYellowColorCode(homeRuns, 3.5f, 6.5f, true);
            }
            else
            {
                // Track winner separately for the winner CSS class
                if (awayRuns > homeRuns) awayWinner = true;
                else if (homeRuns > awayRuns) homeWinner = true;

                // Color based on runs vs league average, scaled to game progress
                float avgRuns = 4.5f * (float)Math.Min(1, game.CurrentOuts / 54.0);

                if (awayRuns >= avgRuns)
                    awayColor = ColorHelper.GetGreenColorCode(awayRuns - avgRuns, 0f, avgRuns, true);
                else
                    awayColor = ColorHelper.GetRedColorCode(avgRuns - awayRuns, 0f, avgRuns, true);

                if (homeRuns >= avgRuns)
                    homeColor = ColorHelper.GetGreenColorCode(homeRuns - avgRuns, 0f, avgRuns, true);
                else
                    homeColor = ColorHelper.GetRedColorCode(avgRuns - homeRuns, 0f, avgRuns, true);
            }

            row.Append(BuildTeamCell(game.AwayTeamCode, awayRuns, awayColor, awayWinner, gameStarted, game.IsGameFinished, game.AwayTeamLineupConfirmed, game.WarningPlayers, game.WarningPlayersType));
            row.Append(BuildTeamCell(game.HomeTeamCode, homeRuns, homeColor, homeWinner, gameStarted, game.IsGameFinished, game.HomeTeamLineupConfirmed, game.WarningPlayers, game.WarningPlayersType));

            if (game.Weather != null)
            {
                var weather = new HtmlTag("div").AddClass("game-date-weather");

                bool isIndoor = game.Weather.StadiumType?.ToLower() == "d";
                bool domeHighOrConfirmed = !string.IsNullOrEmpty(game.Weather.DomeFactor) &&
                    (game.Weather.DomeFactor.ToLower() == "high" || game.Weather.DomeFactor.ToLower() == "confirmed");
                bool skipWeatherIcon = isIndoor || domeHighOrConfirmed || game.Weather.AvgTemp == 0;

                if (!skipWeatherIcon)
                {
                    var rainHoursText = game.Weather.RainHours > 0 ? $" for {game.Weather.RainHours}h" : "";
                    var rainBars = game.Weather.RainChance >= 10 && game.Weather.HourlyRainChance != null && game.Weather.HourlyRainChance.Count > 0
                        ? BuildRainBars(game.Weather.RainChance, game.Weather.HourlyRainChance, false)
                        : "";
                    var tooltipContent = $"{game.Weather.AvgTemp}° · H{game.Weather.AvgHumidity}% · Rain {game.Weather.RainChance}%{rainHoursText} {rainBars}";
                    var weatherIcon = new Icon(new IconInput { Type = IconType.Weather, Size = 16, Color = "#378ADD" }).Render();
                    weather.AppendHtml(new CustomTooltip(weatherIcon, tooltipContent).Render());
                }

                string windColor = null;
                string windStroke = null;

                if (!isIndoor)
                {
                    switch (game.Weather.WindFactor?.ToLower())
                    {
                        case "low": windColor = "#888780"; windStroke = "#5F5E5A"; break;
                        case "medium": windColor = "#F59E0B"; windStroke = "#D97706"; break;
                        case "high": windColor = "#E24B4A"; windStroke = "#A32D2D"; break;
                    }

                    if (windColor != null)
                    {
                        var windArrow = new FieldWindArrow((int)game.Weather.WindFieldDegrees)
                            .WithSize(28)
                            .WithColor(windColor)
                            .WithStrokeColor(windStroke)
                            .Render();
                        if (!skipWeatherIcon) weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                        var windTooltipText = !string.IsNullOrEmpty(game.Weather.WindField)
                            ? $"{game.Weather.WindSpeed}mph {game.Weather.WindField}"
                            : $"{game.Weather.WindSpeed}mph";
                        weather.AppendHtml(new CustomTooltip(windArrow, windTooltipText).WithCentered().Render());
                    }
                }

                if (isIndoor)
                {
                    var domeIcon = new Icon(new IconInput
                    {
                        Type = IconType.Dome,
                        Color = "#FB7185",
                        Fill = "#FB718526",
                        Size = 20
                    }).Render();
                    weather.AppendHtml(new CustomTooltip(domeIcon, "Stadium is a dome.").Render());
                }
                else if (!string.IsNullOrEmpty(game.Weather.DomeFactor) && game.Weather.DomeFactor.ToLower() != "none")
                {
                    string domeColor;
                    string domeTooltip;
                    switch (game.Weather.DomeFactor.ToLower())
                    {
                        case "low":
                            domeColor = "#888780";
                            domeTooltip = "Stadium is expected to be open.";
                            break;
                        case "medium":
                            domeColor = "#F59E0B";
                            domeTooltip = "Stadium may be closed.";
                            break;
                        case "high":
                            domeColor = "#FB7185";
                            domeTooltip = "Stadium is expected to be closed.";
                            break;
                        case "confirmed":
                            domeColor = "#FB7185";
                            domeTooltip = "Stadium will be closed.";
                            break;
                        default:
                            domeColor = null;
                            domeTooltip = null;
                            break;
                    }

                    if (domeColor != null)
                    {
                        var isConfirmed = game.Weather.DomeFactor.ToLower() == "confirmed";
                        var domeIcon = new Icon(new IconInput
                        {
                            Type = isConfirmed ? IconType.Dome : IconType.RetractableDome,
                            Color = domeColor,
                            Fill = isConfirmed ? domeColor + "26" : "none",
                            Size = isConfirmed ? 20 : 24
                        }).Render();

                        if (!skipWeatherIcon || windColor != null)
                            weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));

                        weather.AppendHtml(new CustomTooltip(domeIcon, domeTooltip).Render());
                    }
                }

                row.Append(weather);
            }

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