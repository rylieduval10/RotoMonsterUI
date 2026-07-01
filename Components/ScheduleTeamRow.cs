using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class ScheduleTeamRow
    {
        private readonly ScheduleTeamRowInput _input;

        private static readonly string ExpandChevronSvg =
            "<svg xmlns='http://www.w3.org/2000/svg' width='10' height='10' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2.5' stroke-linecap='round' stroke-linejoin='round'><polyline points='6 9 12 15 18 9'/></svg>";

        public ScheduleTeamRow(ScheduleTeamRowInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("schedule-team-row-wrapper").Attr("id", _input.Id);
            var table = new HtmlTag("table").AddClass("schedule-team-row");
            var tbody = new HtmlTag("tbody");

            var teamRow = new HtmlTag("tr");
            teamRow.Append(new HtmlTag("td").AddClass("schedule-team-row-code").Text(_input.TeamCode));

            foreach (var period in _input.Periods.OrderBy(p => p.PeriodNumber))
            {
                _input.PeriodCells.TryGetValue(period.PeriodNumber, out var cellData);
                var isCurrent = _input.CurrentPeriodNumber.HasValue && _input.CurrentPeriodNumber.Value == period.PeriodNumber;
                var isExpanded = _input.ExpandedPeriodNumber.HasValue && _input.ExpandedPeriodNumber.Value == period.PeriodNumber;

                var cell = new HtmlTag("td").AddClass("schedule-team-row-cell");
                if (isCurrent) cell.AddClass("schedule-team-row-current");

                if (cellData != null)
                {
                    if (_input.ColorType == ScheduleGridColorType.MaxWeeks && cellData.IsMaxWeek)
                        cell.AddClass("schedule-grid-maxweek");
                    else if (_input.ColorType == ScheduleGridColorType.Ease && !string.IsNullOrEmpty(cellData.EaseColor))
                        cell.Attr("style", $"background-color:#{cellData.EaseColor};");

                    var inner = new HtmlTag("button")
                        .AddClass("schedule-team-row-cell-btn")
                        .Attr("type", "button")
                        .Attr("name", $"{_input.Id}-expand")
                        .Attr("value", isExpanded ? "collapse" : period.PeriodNumber.ToString());

                    inner.AppendHtml($"<span>{cellData.Games}</span>");
                    if (cellData.Days.Count > 0)
                        inner.AppendHtml(ExpandChevronSvg);

                    cell.Append(inner);
                }

                teamRow.Append(cell);
            }

            tbody.Append(teamRow);
            table.Append(tbody);
            wrapper.Append(table);

            // Expanded day breakdown for whichever period is currently expanded
            if (_input.ExpandedPeriodNumber.HasValue
                && _input.PeriodCells.TryGetValue(_input.ExpandedPeriodNumber.Value, out var expandedCell)
                && expandedCell.Days.Count > 0)
            {
                wrapper.Append(RenderExpandedDays(expandedCell));
            }

            return wrapper.ToString();
        }

        private HtmlTag RenderExpandedDays(ScheduleGridPeriodCell cellData)
        {
            var container = new HtmlTag("div").AddClass("schedule-team-row-days");

            foreach (var day in cellData.Days.OrderBy(d => d.Date))
            {
                var dayRow = new HtmlTag("div").AddClass("schedule-team-row-day");
                if (day.IsQualityGame)
                    dayRow.AddClass("schedule-team-row-day-quality");

                dayRow.Append(new HtmlTag("span").AddClass("schedule-team-row-day-date").Text(day.Date.ToString("ddd M/d")));

                var opponent = new HtmlTag("span").AddClass("schedule-team-row-day-opponent").Text(day.Opponent);
                if (!string.IsNullOrEmpty(day.EaseColor))
                    opponent.Attr("style", $"background-color:#{day.EaseColor};");
                dayRow.Append(opponent);

                container.Append(dayRow);
            }

            return container;
        }
    }
}