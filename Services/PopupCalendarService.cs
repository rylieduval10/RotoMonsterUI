using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PopupCalendarService
    {
        public PopupCalendarResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new PopupCalendarResult();

            if (formValues.ContainsKey($"{controlId}-prev"))
                result.PrevMonthPressed = true;

            if (formValues.ContainsKey($"{controlId}-next"))
                result.NextMonthPressed = true;

            if (formValues.TryGetValue($"{controlId}-selected", out var selectedStr) && DateTime.TryParse(selectedStr, out var selected))
                result.SelectedDate = selected;

            if (formValues.TryGetValue($"{controlId}-start", out var startStr) && DateTime.TryParse(startStr, out var start))
                result.StartDate = start;

            if (formValues.TryGetValue($"{controlId}-end", out var endStr) && DateTime.TryParse(endStr, out var end))
                result.EndDate = end;

            return result;
        }
    }
}