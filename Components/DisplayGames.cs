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

        public DisplayGames(List<DisplayGameInput> games, string id = "game-date-control")
        {
            _games = games;
            _id = id;
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
                var percent = GetInningPercent(game.CurrentInning);
                var label = game.CurrentInning == 0 ? "1st" : OrdinalHelper.GetOrdinal(game.CurrentInning);

                var progressWrapper = new HtmlTag("div").AddClass("game-state-progress");
                var bar = new HtmlTag("div")
                    .AddClass("game-state-progress-bar")
                    .Attr("style", $"width:{percent}%");
                var text = new HtmlTag("span").AddClass("game-state-progress-text").Text(label);
                progressWrapper.Append(bar);
                progressWrapper.Append(text);
                wrapper.Append(progressWrapper);
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
                var dot = new HtmlTag("span").AddClass(lineupConfirmed ? "lineup-dot lineup-dot-confirmed" : "lineup-dot lineup-dot-empty");
                cell.Append(dot);
            }

            cell.Append(new HtmlTag("span").AddClass("game-team-code").Text(teamCode));

            if (runs != 0)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(runs.ToString("0.#")));

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

        private string BuildRainBars(double rainChance, List<int> hourlyRainChance)
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

                string color = "#" + ColorHelper.GetBlueColorCode(barValue, 0f, 100f, true);
                int barHeight = Math.Max(2, (int)(maxHeight * (barValue / 100.0)));
                int x = i * (barWidth + barGap);
                int y = maxHeight - barHeight;
                sb.Append($"<rect x=\"{x}\" y=\"{y}\" width=\"{barWidth}\" height=\"{barHeight}\" fill=\"{color}\" rx=\"1\"/>");
            }

            sb.Append("</svg>");
            return sb.ToString();
        }

        private HtmlTag BuildGameRow(DisplayGameInput game)
        {
            bool gameStarted = game.IsGameLive || game.IsGameFinished;

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
                var windBadgeInput = new BadgeInput() { BadgeText = new FieldWindArrow((int)game.Weather.WindFieldDegrees).WithSize(16).WithColor($"#{ColorHelper.Black}").Render(), Color = windColor, TooltipText = $"{game.Weather.WindSpeed}mph" };
                var windBadge = new Badge(windBadgeInput);

                var weather = new HtmlTag("div").AddClass("game-date-weather");
                weather.Append(new HtmlTag("span").AddClass("game-date-temp").Text($"{game.Weather.AvgTemp}°"));
                weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                weather.Append(new HtmlTag("span").AddClass("game-date-humidity").Text($"H{game.Weather.AvgHumidity}%"));
                weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                weather.Append(new HtmlTag("span").AddClass("game-date-wind").AppendHtml(windBadge.Render()));
                weather.Append(new HtmlTag("span").AddClass("game-date-wind-speed").Style("color", windColor).Text($"{game.Weather.WindSpeed}mph"));

                if (game.Weather.RainChance > 0)
                {
                    var rainIcon = new Icon(new IconInput { Type = IconType.Rain, Color = "#378ADD" }).Render();
                    weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    weather.Append(new HtmlTag("span").AddClass("game-date-rain").AppendHtml(rainIcon));
                    weather.Append(new HtmlTag("span").AddClass("game-date-rain").AppendHtml(BuildRainBars(game.Weather.RainChance, game.Weather.HourlyRainChance)));
                    weather.Append(new HtmlTag("span").AddClass("game-date-rain").Text($"{game.Weather.RainChance}%"));
                }

                var postponeColor = GetPostponementColor(game.Weather.ChanceOfPostponement);
                if (postponeColor != null)
                {
                    var postponeIcon = new Icon(new IconInput { Type = IconType.PostponementChanceWarning, Color = postponeColor, Fill = postponeColor }).Render();
                    weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    weather.Append(new HtmlTag("span").AddClass("game-date-postpone").AppendHtml(postponeIcon));
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

            return wrapper.ToString();
        }
    }
}