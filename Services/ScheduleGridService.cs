using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ScheduleGridResult
    {
        public ScheduleGridSortBy? SortBy { get; set; }
        public ScheduleGridColorType? ColorType { get; set; }
        public int? StartSelectedPeriod { get; set; }
        public int? EndSelectedPeriod { get; set; }
        public string EasePositionFilterValue { get; set; }
        public int? ExpandedPeriodNumber { get; set; }
        public bool IsCollapseRequested { get; set; }
    }

    public class ScheduleGridService
    {
        public ScheduleGridResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new ScheduleGridResult();

            if (formValues.TryGetValue($"{controlId}-sortby", out var sortBy)
                && System.Enum.TryParse<ScheduleGridSortBy>(sortBy, true, out var parsedSortBy))
            {
                result.SortBy = parsedSortBy;
            }

            if (formValues.TryGetValue($"{controlId}-colortype", out var colorType)
                && System.Enum.TryParse<ScheduleGridColorType>(colorType, true, out var parsedColorType))
            {
                result.ColorType = parsedColorType;
            }

            // Up/down arrows on a period row set that period as the new start/end of the selected range
            foreach (var kvp in formValues)
            {
                if (kvp.Key == $"{controlId}-set-start" && int.TryParse(kvp.Value, out var startPeriod))
                    result.StartSelectedPeriod = startPeriod;

                if (kvp.Key == $"{controlId}-set-end" && int.TryParse(kvp.Value, out var endPeriod))
                    result.EndSelectedPeriod = endPeriod;
            }

            if (formValues.TryGetValue($"{controlId}-ease-position", out var easePosition))
                result.EasePositionFilterValue = easePosition;

            if (formValues.TryGetValue($"{controlId}-expand", out var expandValue))
            {
                if (int.TryParse(expandValue, out var expandPeriod))
                    result.ExpandedPeriodNumber = expandPeriod;
                else
                    result.IsCollapseRequested = true;
            }

            return result;
        }
    }
}