using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class PopupCalendar
    {
        private readonly PopupCalendarInput _input;

        public PopupCalendar(PopupCalendarInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapperId = _input.Id;
            var panelId = $"{wrapperId}-panel";

            var wrapper = new HtmlTag("div")
                .AddClass("popup-cal-wrapper")
                .Attr("id", wrapperId);

            // Display values
            var displayDate = _input.SelectedDate ?? DateTime.Today;
            var displayDay = displayDate.Day;
            var triggerLabel = displayDate.ToString("MMM d, yyyy");

            // Calendar icon with day number
            var calIcon = $@"<svg width=""18"" height=""18"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round""><rect x=""3"" y=""4"" width=""18"" height=""18"" rx=""2""/><line x1=""3"" y1=""9"" x2=""21"" y2=""9""/><line x1=""8"" y1=""2"" x2=""8"" y2=""6""/><line x1=""16"" y1=""2"" x2=""16"" y2=""6""/><text x=""12"" y=""19"" text-anchor=""middle"" font-size=""8.5"" font-weight=""bold"" stroke=""none"" fill=""currentColor"" font-family=""system-ui"">{displayDay}</text></svg>";

            // Trigger button
            var trigger = new HtmlTag("button")
                .AddClass("popup-cal-trigger modern-filter-btn modern-filter-btn-secondary")
                .Attr("type", "button")
                .Attr("data-popup-cal", panelId);
            trigger.AppendHtml($"<span style='margin-right:0.35rem'>{calIcon}</span><span class='popup-cal-trigger-label'>{triggerLabel}</span>");
            wrapper.Append(trigger);

            // Hidden input for selected date
            var month = _input.DisplayMonth;
            wrapper.Append(new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("id", $"{wrapperId}-selected")
                .Attr("name", $"{wrapperId}-selected")
                .Attr("value", displayDate.ToString("yyyy-MM-dd")));

            // Popup panel
            var panel = new HtmlTag("div")
                .AddClass("popup-cal-panel")
                .Id(panelId);

            // Month header
            var header = new HtmlTag("div").AddClass("popup-cal-header");

            var prevIcon = new Icon(new IconInput { Type = IconType.Previous, Size = 16 }).Render();
            header.Append(new HtmlTag("button")
                .AddClass("popup-cal-nav-btn")
                .Attr("type", "submit")
                .Attr("name", $"{wrapperId}-prev")
                .Attr("value", "1")
                .AppendHtml(prevIcon));

            header.Append(new HtmlTag("span")
                .AddClass("popup-cal-month-label")
                .Text($"{month:MMMM yyyy}"));

            var nextIcon = new Icon(new IconInput { Type = IconType.Next, Size = 16 }).Render();
            header.Append(new HtmlTag("button")
                .AddClass("popup-cal-nav-btn")
                .Attr("type", "submit")
                .Attr("name", $"{wrapperId}-next")
                .Attr("value", "1")
                .AppendHtml(nextIcon));

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
                if (_input.SelectedDate.HasValue && date.Date == _input.SelectedDate.Value.Date)
                    classes += " popup-cal-day--selected";
                if (_input.MinDate.HasValue && date < _input.MinDate.Value)
                    classes += " popup-cal-day--disabled";
                if (_input.MaxDate.HasValue && date > _input.MaxDate.Value)
                    classes += " popup-cal-day--disabled";

                var btn = new HtmlTag("button")
                    .AddClass(classes)
                    .Attr("type", "button")
                    .Attr("data-popup-cal-date", date.ToString("yyyy-MM-dd"))
                    .Attr("data-popup-cal-target", $"{wrapperId}-selected")
                    .Text(day.ToString());

                grid.Append(btn);
            }

            panel.Append(grid);
            wrapper.Append(panel);

            return wrapper.ToString();
        }
    }
}