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

        public DisplayGames(List<DisplayGameInput> games, string id = "game-date-control", bool showSettingsLink = false)
        {
            _games = games;
            _id = id;
            _showSettingsLink = showSettingsLink;
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

                // Postponement flag inside time box -- only show pre-game
                if (game.Weather != null)
                {
                    var postponeColor = GetPostponementColor(game.Weather?.PostponementFactor);
                    if (postponeColor != null)
                    {
                        var postponeIcon = new Icon(new IconInput { Type = IconType.PostponementChanceWarning, Color = postponeColor, Fill = postponeColor, Size = 18 }).Render();
                        var postponeLabel = string.IsNullOrEmpty(game.Weather.PostponementFactor) ? ""
                            : char.ToUpper(game.Weather.PostponementFactor[0]) + game.Weather.PostponementFactor.Substring(1).ToLower();
                        var postponeWrapper = new HtmlTag("span")
                            .AddClass("game-date-postpone")
                            .Attr("data-toggle", "tooltip")
                            .Attr("data-placement", "top")
                            .Attr("title", $"Postponement: {postponeLabel}")
                            .AppendHtml(postponeIcon);
                        upcoming.AppendHtml("&nbsp;");
                        upcoming.Append(postponeWrapper);
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

        private HtmlTag BuildTeamCell(string teamCode, float runs, string bgColor, bool isWinner, bool gameStarted, bool isGameFinished, bool lineupConfirmed)
        {
            var cell = new HtmlTag("div").AddClass("game-team-cell");
            if (isWinner && isGameFinished)
                cell.AddClass("winner");
            cell.Attr("style", $"background-color:#{bgColor};");

            if (!gameStarted)
            {
                var dot = new HtmlTag("span")
                    .AddClass(lineupConfirmed ? "lineup-dot lineup-dot-confirmed" : "lineup-dot lineup-dot-empty")
                    .Attr("data-toggle", "tooltip")
                    .Attr("data-placement", "top")
                    .Attr("title", lineupConfirmed ? "Lineup Confirmed" : "Lineup Not Confirmed");
                cell.Append(dot);
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

        private HtmlTag BuildGameRow(DisplayGameInput game)
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
                float diff = Math.Abs(awayRuns - homeRuns);
                if (awayRuns > homeRuns)
                {
                    awayWinner = true;
                    awayColor = ColorHelper.GetGreenColorCode(diff, 0f, 10f, true);
                    homeColor = ColorHelper.GetRedColorCode(diff, 0f, 10f, true);
                }
                else if (homeRuns > awayRuns)
                {
                    homeWinner = true;
                    homeColor = ColorHelper.GetGreenColorCode(diff, 0f, 10f, true);
                    awayColor = ColorHelper.GetRedColorCode(diff, 0f, 10f, true);
                }
                else
                {
                    awayColor = ColorHelper.GetGreenColorCode(0f, 0f, 10f, true);
                    homeColor = ColorHelper.GetGreenColorCode(0f, 0f, 10f, true);
                }
            }

            row.Append(BuildTeamCell(game.AwayTeamCode, awayRuns, awayColor, awayWinner, gameStarted, game.IsGameFinished, game.AwayTeamLineupConfirmed));
            row.Append(BuildTeamCell(game.HomeTeamCode, homeRuns, homeColor, homeWinner, gameStarted, game.IsGameFinished, game.HomeTeamLineupConfirmed));

            if (game.Weather != null && game.Weather.StadiumType?.ToUpper() != "D")
            {
                var weather = new HtmlTag("div").AddClass("game-date-weather");

                bool isIndoor = game.Weather.StadiumType?.ToLower() == "indoor";
                bool domeHighOrConfirmed = !string.IsNullOrEmpty(game.Weather.DomeFactor) &&
                    (game.Weather.DomeFactor.ToLower() == "high" || game.Weather.DomeFactor.ToLower() == "confirmed");
                bool skipWeatherIcon = isIndoor || domeHighOrConfirmed;

                if (!skipWeatherIcon)
                {
                    var rainHoursText = game.Weather.RainHours > 0 ? $" for {game.Weather.RainHours}h" : "";
                    var rainBars = game.Weather.HourlyRainChance != null && game.Weather.HourlyRainChance.Count > 0
                        ? BuildRainBars(game.Weather.RainChance, game.Weather.HourlyRainChance, false)
                        : "";
                    var tooltipContent = $"{game.Weather.AvgTemp}° · H{game.Weather.AvgHumidity}% · Rain {game.Weather.RainChance}%{rainHoursText} {rainBars}";
                    var weatherIcon = new Icon(new IconInput { Type = IconType.Weather, Size = 16, Color = "#378ADD" }).Render();
                    var weatherIconWrapper = new HtmlTag("span")
                        .Attr("data-toggle", "tooltip")
                        .Attr("data-placement", "top")
                        .Attr("data-html", "true")
                        .Attr("title", tooltipContent)
                        .AppendHtml(weatherIcon);
                    weather.Append(weatherIconWrapper);
                }

                if (!isIndoor)
                {
                    string windColor;
                    string windStroke;
                    switch (game.Weather.WindFactor?.ToLower())
                    {
                        case "low": windColor = "#888780"; windStroke = "#5F5E5A"; break;
                        case "medium": windColor = "#F59E0B"; windStroke = "#D97706"; break;
                        case "high": windColor = "#E24B4A"; windStroke = "#A32D2D"; break;
                        default: windColor = null; windStroke = null; break;
                    }

                    if (windColor != null)
                    {
                        var windArrow = new FieldWindArrow((int)game.Weather.WindFieldDegrees)
                            .WithSize(28)
                            .WithColor(windColor)
                            .WithStrokeColor(windStroke)
                            .Render();
                        var windWrapper = new HtmlTag("span")
                            .Attr("data-toggle", "tooltip")
                            .Attr("data-placement", "top")
                            .Attr("title", $"{game.Weather.WindSpeed}mph")
                            .AppendHtml(windArrow);
                        if (!skipWeatherIcon) weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                        weather.Append(windWrapper);
                    }
                }

                // Dome icon
                if (isIndoor)
                {
                    var domeIcon = new Icon(new IconInput
                    {
                        Type = IconType.Dome,
                        Color = "#888780",
                        Fill = "#88878026",
                        Size = 20
                    }).Render();
                    var domeWrapper = new HtmlTag("span")
                        .Attr("data-toggle", "tooltip")
                        .Attr("data-placement", "top")
                        .Attr("title", "Stadium will be closed.")
                        .AppendHtml(domeIcon);
                    weather.Append(domeWrapper);
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
                            domeColor = "#888780";
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
                        var domeWrapper = new HtmlTag("span")
                            .Attr("data-toggle", "tooltip")
                            .Attr("data-placement", "top")
                            .Attr("title", domeTooltip)
                            .AppendHtml(domeIcon);
                        weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                        weather.Append(domeWrapper);
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