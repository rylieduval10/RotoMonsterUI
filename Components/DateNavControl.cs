using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class DateNavControl
    {
        private DateNavControlInput _input;

        public DateNavControl(DateNavControlInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var id = _input.Id;
            var panelId = $"{id}-cal-panel";
            var dateFormat = _input.ShowDayOfWeek ? "dddd, MMMM d" : "MMMM d";

            var wrapper = new HtmlTag("div").AddClass("date-nav-control").Attr("id", id);

            // Prev button - submit to go to previous day
            var prevIcon = new Icon(new IconInput { Type = IconType.Previous, Size = 16 }).Render();
            var prev = new HtmlTag("button")
                .AddClass("date-nav-btn")
                .Attr("type", "submit")
                .Attr("name", $"{id}-prev")
                .Attr("value", "1")
                .AppendHtml(prevIcon);
            wrapper.Append(prev);

            // Next button - submit to go to next day (moved up, now next to prev)
            var nextIcon = new Icon(new IconInput { Type = IconType.Next, Size = 16 }).Render();
            var next = new HtmlTag("button")
                .AddClass("date-nav-btn")
                .Attr("type", "submit")
                .Attr("name", $"{id}-next")
                .Attr("value", "1")
                .AppendHtml(nextIcon);
            wrapper.Append(next);

            // Calendar trigger button
            var displayDay = _input.SelectedDate.Day;
            var calIcon = $@"<svg width=""18"" height=""18"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round""><rect x=""3"" y=""4"" width=""18"" height=""18"" rx=""2""/><line x1=""3"" y1=""9"" x2=""21"" y2=""9""/><line x1=""8"" y1=""2"" x2=""8"" y2=""6""/><line x1=""16"" y1=""2"" x2=""16"" y2=""6""/><text x=""12"" y=""19"" text-anchor=""middle"" font-size=""8.5"" font-weight=""bold"" stroke=""none"" fill=""currentColor"" font-family=""system-ui"">{displayDay}</text></svg>";

            var trigger = new HtmlTag("button")
                .AddClass("popup-cal-trigger modern-filter-btn modern-filter-btn-secondary")
                .Attr("type", "button")
                .Attr("data-popup-cal", panelId)
                .Attr("data-date-nav-cal", id);
            trigger.AppendHtml($"<span style='margin-right:0.35rem'>{calIcon}</span><span class='popup-cal-trigger-label'>{_input.SelectedDate.ToString(dateFormat)}</span>");
            wrapper.Append(trigger);

            // Game count
            if (_input.GameCount > 0)
            {
                var gameCount = new HtmlTag("span")
                    .AddClass("date-nav-game-count")
                    .Text($"{_input.GameCount} Games");
                wrapper.Append(gameCount);
            }

            // Refresh button - submit to refresh with current date
            if (_input.ShowRefresh)
            {
                var refresh = new HtmlTag("button")
                    .AddClass("date-nav-btn")
                    .Attr("type", "submit")
                    .Attr("name", $"{id}-refresh")
                    .Attr("value", "1")
                    .Text("Refresh");
                wrapper.Append(refresh);
            }

            // Reset button - submit to reset to original date
            var reset = new HtmlTag("button")
                .AddClass("date-nav-btn")
                .Attr("type", "submit")
                .Attr("name", $"{id}-reset")
                .Attr("value", "1")
                .Text("Reset");
            wrapper.Append(reset);

            // Hidden date field
            wrapper.Append(new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("id", $"{id}-date")
                .Attr("name", $"{id}-date")
                .Attr("value", _input.SelectedDate.ToString("yyyy-MM-dd")));

            // Hidden original date field for reset
            wrapper.Append(new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("id", $"{id}-original-date")
                .Attr("name", $"{id}-original-date")
                .Attr("value", _input.OriginalDate.ToString("yyyy-MM-dd")));

            // Calendar panel
            var month = _input.SelectedDate;
            var panel = new HtmlTag("div")
                .AddClass("popup-cal-panel")
                .Attr("id", panelId)
                .Attr("data-month", month.ToString("yyyy-MM"))
                .Attr("data-date-nav-target", $"{id}-date")
                .Attr("data-date-nav-trigger", id);

            // Month header
            var header = new HtmlTag("div").AddClass("popup-cal-header");
            var pIcon = new Icon(new IconInput { Type = IconType.Previous, Size = 16 }).Render();
            header.Append(new HtmlTag("button")
                .AddClass("popup-cal-nav-btn")
                .Attr("type", "button")
                .Attr("data-popup-cal-prev", id)
                .AppendHtml(pIcon));
            header.Append(new HtmlTag("span")
                .AddClass("popup-cal-month-label")
                .Text($"{month:MMMM yyyy}"));
            var nIcon = new Icon(new IconInput { Type = IconType.Next, Size = 16 }).Render();
            header.Append(new HtmlTag("button")
                .AddClass("popup-cal-nav-btn")
                .Attr("type", "button")
                .Attr("data-popup-cal-next", id)
                .AppendHtml(nIcon));
            panel.Append(header);

            // Day labels
            var dayLabels = new HtmlTag("div").AddClass("popup-cal-day-labels");
            foreach (var d in new[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" })
                dayLabels.Append(new HtmlTag("span").AddClass("popup-cal-day-label").Text(d));
            panel.Append(dayLabels);

            // Day grid
            var grid = new HtmlTag("div").AddClass("popup-cal-grid");
            var firstOfMonth = new DateTime(month.Year, month.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
            var startDayOfWeek = (int)firstOfMonth.DayOfWeek;
            var today = DateTime.Today;

            for (int i = 0; i < startDayOfWeek; i++)
                grid.Append(new HtmlTag("span").AddClass("popup-cal-day popup-cal-day--empty"));

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(month.Year, month.Month, day);
                var classes = "popup-cal-day";
                if (date == today) classes += " popup-cal-day--today";
                if (date.Date == _input.SelectedDate.Date) classes += " popup-cal-day--selected";

                var btn = new HtmlTag("button")
                    .AddClass(classes)
                    .Attr("type", "button")
                    .Attr("data-popup-cal-date", date.ToString("yyyy-MM-dd"))
                    .Attr("data-popup-cal-target", $"{id}-date")
                    .Attr("data-date-nav-cal", id)
                    .Text(day.ToString());
                grid.Append(btn);
            }

            panel.Append(grid);
            wrapper.Append(panel);

            return wrapper.ToString();
        }
    }
}