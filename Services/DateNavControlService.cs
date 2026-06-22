using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DateNavControlService
    {
        public DateNavControlResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var prevPressed = formValues.ContainsKey($"{controlId}-prev");
            var nextPressed = formValues.ContainsKey($"{controlId}-next");
            var refreshPressed = formValues.ContainsKey($"{controlId}-refresh");

            DateTime? newDate = null;
            if (formValues.TryGetValue($"{controlId}-date", out var dateStr) && DateTime.TryParse(dateStr, out var currentDate))
            {
                if (prevPressed) newDate = currentDate.AddDays(-1);
                else if (nextPressed) newDate = currentDate.AddDays(1);
                else newDate = currentDate;
            }

            return new DateNavControlResult
            {
                PrevDatePressed = prevPressed,
                NextDatePressed = nextPressed,
                RefreshPressed = refreshPressed,
                NewSelectedDate = newDate
            };
        }
    }
}