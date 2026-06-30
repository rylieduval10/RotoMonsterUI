using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class Weather
    {
        private readonly WeatherInput _input;

        public Weather(WeatherInput input)
        {
            _input = input;
        }

        public string Render()
        {
            if (_input == null) return "";

            var weather = new HtmlTag("div").AddClass("game-date-weather");
            bool isIndoor = _input.StadiumType?.ToLower() == "d";
            bool domeHighOrConfirmed = !string.IsNullOrEmpty(_input.DomeFactor) &&
                (_input.DomeFactor.ToLower() == "high" || _input.DomeFactor.ToLower() == "confirmed");
            bool skipWeatherIcon = isIndoor || domeHighOrConfirmed || _input.AvgTemp == 0;

            if (!skipWeatherIcon)
            {
                var rainHoursText = _input.RainHours > 0 ? $" for {_input.RainHours}h" : "";
                var rainBars = _input.RainChance >= 10 && _input.HourlyRainChance != null && _input.HourlyRainChance.Count > 0
                    ? BuildRainBars(_input.RainChance, _input.HourlyRainChance, false)
                    : "";
                var tooltipContent = $"{_input.AvgTemp}° · H{_input.AvgHumidity}% · Rain {_input.RainChance}%{rainHoursText} {rainBars}";
                var weatherIcon = new Icon(new IconInput { Type = IconType.Weather, Size = 16, Color = "#378ADD" }).Render();
                weather.AppendHtml(new CustomTooltip(weatherIcon, tooltipContent).Render());
            }

            string windColor = null;
            string windStroke = null;

            if (!isIndoor)
            {
                switch (_input.WindFactor?.ToLower())
                {
                    case "low": windColor = "#888780"; windStroke = "#5F5E5A"; break;
                    case "medium": windColor = "#F59E0B"; windStroke = "#D97706"; break;
                    case "high": windColor = "#E24B4A"; windStroke = "#A32D2D"; break;
                }

                if (windColor != null)
                {
                    var windArrow = new FieldWindArrow((int)_input.WindFieldDegrees)
                        .WithSize(28)
                        .WithColor(windColor)
                        .WithStrokeColor(windStroke)
                        .Render();
                    if (!skipWeatherIcon) weather.Append(new HtmlTag("span").AddClass("game-date-sep").Text("·"));
                    var windTooltipText = !string.IsNullOrEmpty(_input.WindField)
                        ? $"{_input.WindSpeed}mph {_input.WindField}"
                        : $"{_input.WindSpeed}mph";
                    weather.AppendHtml(new CustomTooltip(windArrow, windTooltipText).WithCentered().Render());
                }
            }

            if (isIndoor)
            {
                var domeIcon = new Icon(new IconInput { Type = IconType.Dome, Color = "#FB7185", Fill = "#FB718526", Size = 20 }).Render();
                weather.AppendHtml(new CustomTooltip(domeIcon, "Stadium is a dome.").Render());
            }
            else if (!string.IsNullOrEmpty(_input.DomeFactor) && _input.DomeFactor.ToLower() != "none")
            {
                string domeColor;
                string domeTooltip;
                switch (_input.DomeFactor.ToLower())
                {
                    case "low": domeColor = "#888780"; domeTooltip = "Stadium is expected to be open."; break;
                    case "medium": domeColor = "#F59E0B"; domeTooltip = "Stadium may be closed."; break;
                    case "high": domeColor = "#FB7185"; domeTooltip = "Stadium is expected to be closed."; break;
                    case "confirmed": domeColor = "#FB7185"; domeTooltip = "Stadium will be closed."; break;
                    default: domeColor = null; domeTooltip = null; break;
                }

                if (domeColor != null)
                {
                    var isConfirmed = _input.DomeFactor.ToLower() == "confirmed";
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

            return weather.ToString();
        }

        private string BuildRainBars(double rainChance, System.Collections.Generic.List<int> hourlyRainChance, bool whiteMode)
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
    }
}