using System;
using System.Collections.Generic;
using HtmlTags;

namespace RotoMonsterUI
{
    public class CalendarPicker
    {
        private CalendarPickerInput _input;

        public CalendarPicker(CalendarPickerInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var selected = _input.SelectedDate;
            var firstOfMonth = new DateTime(selected.Year, selected.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(selected.Year, selected.Month);
            var startDayOfWeek = (int)firstOfMonth.DayOfWeek;
            var today = DateTime.Today;

            var wrapper = new HtmlTag("div").AddClass("cal-wrapper").Attr("id", _input.Id);

            // Header with prev/next
            var header = new HtmlTag("div").AddClass("cal-header");
            header.Append(new HtmlTag("button")
                .AddClass("cal-nav-btn")
                .Attr("name", $"{_input.Id}-prev")
                .Attr("value", "1")
                .Text("<"));
            header.Append(new HtmlTag("span")
                .AddClass("cal-month-label")
                .Text($"{selected:MMMM yyyy}"));
            header.Append(new HtmlTag("button")
                .AddClass("cal-nav-btn")
                .Attr("name", $"{_input.Id}-next")
                .Attr("value", "1")
                .Text(">"));
            wrapper.Append(header);

            // Day of week labels
            var dayLabels = new HtmlTag("div").AddClass("cal-day-labels");
            var days = new[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
            foreach (var d in days)
                dayLabels.Append(new HtmlTag("span").AddClass("cal-day-label").Text(d));
            wrapper.Append(dayLabels);

            // Day grid
            var grid = new HtmlTag("div").AddClass("cal-grid");

            // Empty cells before first day
            for (int i = 0; i < startDayOfWeek; i++)
                grid.Append(new HtmlTag("span").AddClass("cal-day cal-day--empty"));

            // Day cells
            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(selected.Year, selected.Month, day);
                var classes = "cal-day";

                if (date == today) classes += " cal-day--today";
                if (date.Date == selected.Date) classes += " cal-day--selected";
                if (_input.MinDate.HasValue && date < _input.MinDate.Value) classes += " cal-day--disabled";
                if (_input.MaxDate.HasValue && date > _input.MaxDate.Value) classes += " cal-day--disabled";

                var btn = new HtmlTag("button")
                    .AddClass(classes)
                    .Attr("name", $"{_input.Id}-selected")
                    .Attr("value", date.ToString("yyyy-MM-dd"))
                    .Text(day.ToString());

                grid.Append(btn);
            }

            wrapper.Append(grid);

            return wrapper.ToString();
        }
    }
}