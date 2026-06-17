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

                // Left circles (top of inning)
                var topHalf = new HtmlTag("div").AddClass("game-state-outs-half");
                for (int i = 0; i < 3; i++)
                {
                    var circle = new HtmlTag("span").AddClass(i < topOuts ? "out-circle out-circle--filled" : "out-circle");
                    topHalf.Append(circle);
                }

                // Inning label
                var inningTag = new HtmlTag("span").AddClass("game-state-inning-label").Text(inningLabel);

                // Right circles (bottom of inning)
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
                    upcoming.Attr("style", "border: 1px solid #d1d5db; text-align:center; border-radius:4px; padding: 0.25rem 0.5rem;");
                }

                upcoming.Text($"{timeStr} {untilStr}");
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
                case "low": return "#34D399";
                case "medium": return "#FBBF24";
                case "high": return "#FB7185";
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

            // Auto-detect final if data provider hasn't marked it yet
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

            // Game state
            row.Append(BuildGameState(game));

            // Team cells
            float awayRuns = GetRuns(game.AwayTeamProjectedRuns, game.AwayTeamCurrentRuns, gameStarted);
            float homeRuns = GetRuns(game.HomeTeamProjectedRuns, game.HomeTeamCurrentRuns, gameStarted);

            string awayColor, homeColor;
            bool awayWinner = false, homeWinner = false;

            if (!gameStarted)
            {
                awayColor = ColorHelper.GetGreenColorCode(awayRuns, 3f, 8f, true);
                homeColor = ColorHelper.GetGreenColorCode(homeRuns, 3f, 8f, true);
            }
            else
            {
                float diff = Math.Abs(awayRuns - homeRuns);
                if (awayRuns > homeRuns)
                {
                    awayWinner = true;
                    awayColor = ColorHelper.GetGreenColorCode(diff, 0f, 8f, true);
                    homeColor = ColorHelper.GetRedColorCode(diff, 0f, 8f, true);
                }
                else if (homeRuns > awayRuns)
                {
                    homeWinner = true;
                    homeColor = ColorHelper.GetGreenColorCode(diff, 0f, 8f, true);
                    awayColor = ColorHelper.GetRedColorCode(diff, 0f, 8f, true);
                }
                else
                {
                    awayColor = ColorHelper.GetGreenColorCode(0f, 0f, 8f, true);
                    homeColor = ColorHelper.GetGreenColorCode(0f, 0f, 8f, true);
                }
            }

            row.Append(BuildTeamCell(game.AwayTeamCode, awayRuns, awayColor, awayWinner, gameStarted, game.IsGameFinished, game.AwayTeamLineupConfirmed));
            row.Append(BuildTeamCell(game.HomeTeamCode, homeRuns, homeColor, homeWinner, gameStarted, game.IsGameFinished, game.HomeTeamLineupConfirmed));

            // Weather
            if (game.Weather != null && game.Weather.StadiumType?.ToUpper() != "D")
            {
                var windColor = $"#{ColorHelper.GetRedColorCode(Convert.ToInt32(game.Weather.WindSpeed), 0, 25, true)}";
                var windArrow = new FieldWindArrow((int)game.Weather.WindFieldDegrees).WithSize(40).WithColor(windColor).Render();

                var weather = new HtmlTag("div").AddClass("game-date-weather");
                weather.Append(new HtmlTag("span").AddClass("game-date-temp").Text($"{game.Weather.AvgTemp}°"));
                weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                weather.Append(new HtmlTag("span").AddClass("game-date-humidity").Text($"H{game.Weather.AvgHumidity}%"));
                weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));

                var windWrapper = new HtmlTag("span")
                    .AddClass("game-date-wind")
                    .Attr("data-toggle", "tooltip")
                    .Attr("data-placement", "top")
                    .Attr("title", $"{game.Weather.WindSpeed}mph")
                    .AppendHtml(windArrow);
                weather.Append(windWrapper);

                if (game.Weather.RainChance > 0)
                {
                    bool whiteMode = game.Weather.RainChance >= 30;
                    var rainBars = BuildRainBars(game.Weather.RainChance, game.Weather.HourlyRainChance, whiteMode);

                    var textColor = game.Weather.RainChance >= 30 ? "FFFFFF" : "1e3a8a";
                    var rainIcon = new Icon(new IconInput { Type = IconType.Rain, Color = game.Weather.RainChance >= 30 ? "#FFFFFF" : "#1e3a8a", Size = 18 }).Render();
                    var badgeBgColor = ColorHelper.GetBlueColorCode((float)game.Weather.RainChance, 0f, 100f, true);
                    var rainBadge = new Badge(new BadgeInput
                    {
                        BadgeText = $"<span style='display:inline-flex;align-items:center;gap:3px;line-height:1;'>{rainIcon}<span>{game.Weather.RainChance}%</span>{rainBars}</span>",
                        Color = badgeBgColor,
                        TextColor = textColor
                    });
                    weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    weather.Append(new HtmlTag("span").AddClass("game-date-rain").AppendHtml(rainBadge.Render()));
                }

                var postponeColor = GetPostponementColor(game.Weather.ChanceOfPostponement);
                if (postponeColor != null)
                {
                    var postponeIcon = new Icon(new IconInput { Type = IconType.PostponementChanceWarning, Color = postponeColor, Fill = postponeColor, Size = 16 }).Render();
                    var postponeWrapper = new HtmlTag("span")
                        .AddClass("game-date-postpone")
                        .Attr("data-toggle", "tooltip")
                        .Attr("data-placement", "top")
                        .Attr("title", $"Postponement chance: {game.Weather.ChanceOfPostponement}")
                        .AppendHtml(postponeIcon);
                    weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    weather.Append(postponeWrapper);
                }

                row.Append(weather);
            }

            // View box score
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
