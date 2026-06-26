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

            if (formValues.TryGetValue($"{controlId}-selected", out var dateStr) && DateTime.TryParse(dateStr, out var date))
                result.SelectedDate = date;

            return result;
        }
    }
}