using HtmlTags;
using System;

namespace RotoMonsterUI
{
    public class WeekCalendar
    {
        private readonly WeekCalendarInput _input;

        public WeekCalendar(WeekCalendarInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("week-calendar-wrapper");
            var table = new HtmlTag("table").AddClass("week-calendar");
            var tbody = new HtmlTag("tbody");

            // Week/Date header row
            var headerRow = new HtmlTag("tr");
            headerRow.Append(new HtmlTag("td").AddClass("week-calendar-header").Text("WEEK"));

            foreach (var week in _input.Weeks)
            {
                var td = new HtmlTag("td").AddClass("week-calendar-week-cell");
                if (week.IsCurrentWeek) td.AddClass("week-calendar-current");

                var weekNum = new HtmlTag("div").AddClass("week-calendar-week-num").Text(week.WeekNumber.ToString());
                var date = new HtmlTag("div").AddClass("week-calendar-date").Text(week.StartDate.ToString("M/d"));

                td.Append(weekNum);
                td.Append(date);
                headerRow.Append(td);
            }

            // Games row
            var gamesRow = new HtmlTag("tr");
            gamesRow.Append(new HtmlTag("td").AddClass("week-calendar-header").Text("Games"));

            foreach (var week in _input.Weeks)
            {
                var bgColor = week.GameCount == 0
                    ? ColorHelper.GetRedColorCode(50, 0, 100, true)
                    : ColorHelper.GetGreenColorCode(week.GameCount, 0, 7, true);

                var td = new HtmlTag("td").AddClass("week-calendar-games");
                if (week.IsCurrentWeek) td.AddClass("week-calendar-current");
                td.Attr("style", $"background-color:#{bgColor}; color:#000;");
                td.Text(week.GameCount.ToString());
                gamesRow.Append(td);
            }

            tbody.Append(headerRow);
            tbody.Append(gamesRow);
            table.Append(tbody);
            wrapper.Append(table);

            return wrapper.ToString();
        }
    }
}