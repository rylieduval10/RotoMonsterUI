using HtmlTags;

namespace RotoMonsterUI
{
    public class SeasonProgress
    {
        private readonly SeasonProgressInput _input;

        public SeasonProgress(SeasonProgressInput input)
        {
            _input = input;
        }

        private static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public string Render()
        {
            var showDaysUntil = _input.DaysUntilSeason.HasValue && _input.DaysUntilSeason.Value >= 0;

            var seasonPercent = showDaysUntil ? 0 : Clamp(_input.SeasonPercent, 0, 100);
            var unusedPercent = Clamp(_input.UnusedPercent ?? 0, 0, 100);
            var playoffPercent = Clamp(_input.PlayoffPercent ?? 0, 0, 100 - unusedPercent);

            var unusedLeft = 100 - unusedPercent;
            var playoffLeft = unusedLeft - playoffPercent;

            var bar = new HtmlTag("div").AddClass("season-progress");
            if (showDaysUntil)
                bar.AddClass("season-progress--pending");

            var fill = new HtmlTag("div")
                .AddClass("season-progress-fill")
                .Attr("style", $"width:{seasonPercent.ToString("0.##")}%;");
            bar.Append(fill);

            if (playoffPercent > 0)
            {
                var playoff = new HtmlTag("div")
                    .AddClass("season-progress-playoff")
                    .Attr("style", $"left:{playoffLeft.ToString("0.##")}%; width:{playoffPercent.ToString("0.##")}%;");
                bar.Append(playoff);
            }

            if (unusedPercent > 0)
            {
                var unused = new HtmlTag("div")
                    .AddClass("season-progress-unused")
                    .Attr("style", $"left:{unusedLeft.ToString("0.##")}%; width:{unusedPercent.ToString("0.##")}%;");
                bar.Append(unused);
            }

            var label = _input.Label ?? (showDaysUntil
                ? $"{_input.DaysUntilSeason.Value} {SingularPlural.Get("day", _input.DaysUntilSeason.Value)}"
                : $"Season {seasonPercent.ToString("0.#")}%");
            var labelSpan = new HtmlTag("div")
                .AddClass("season-progress-label");
            if (showDaysUntil)
                labelSpan.AddClass("season-progress-label--pending");
            labelSpan.AppendHtml(label);
            bar.Append(labelSpan);

            if (!_input.ShowTooltip)
                return bar.ToString();

            var tooltipLines = showDaysUntil
                ? $"The regular season begins in {_input.DaysUntilSeason.Value} {SingularPlural.Get("day", _input.DaysUntilSeason.Value)}."
                : $"The Season is {seasonPercent.ToString("0.#")}% complete.";
            if (playoffPercent > 0)
                tooltipLines += "<br />The green section represents your playoffs.";
            if (unusedPercent > 0)
                tooltipLines += "<br />The gray section is the portion of the season after your league is finished, which is set under Settings/League Settings.";

            return new CustomTooltip(bar.ToString(), tooltipLines).WithMaxWidth(280).Render();
        }
    }
}