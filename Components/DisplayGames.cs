using System;
using System.Collections.Generic;
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

        private HtmlTag BuildGameRow(DisplayGameInput game)
        {
            bool gameStarted = game.IsGameLive || game.IsGameFinished;

            var row = new HtmlTag("div").AddClass("game-date-row");

            // Game state
            row.Append(BuildGameState(game));

            // Away team
            var away = new HtmlTag("div").AddClass("game-date-team game-date-away");
            away.Append(new HtmlTag("span").AddClass("game-date-team-code").Text(game.AwayTeamCode));
            away.Append(new HtmlTag("span").AddClass("game-date-runs").Text(GetRuns(game.AwayTeamProjectedRuns, game.AwayTeamCurrentRuns, gameStarted).ToString()));
            row.Append(away);

            // Home team
            var home = new HtmlTag("div").AddClass("game-date-team game-date-home");
            home.Append(new HtmlTag("span").AddClass("game-date-team-code").Text(game.HomeTeamCode));
            home.Append(new HtmlTag("span").AddClass("game-date-runs").Text(GetRuns(game.HomeTeamProjectedRuns, game.HomeTeamCurrentRuns, gameStarted).ToString()));
            row.Append(home);

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
                    weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    weather.Append(new HtmlTag("span").AddClass("game-date-rain").Text($"{game.Weather.RainChance}%"));
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