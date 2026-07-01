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
            var resetPressed = formValues.ContainsKey($"{controlId}-reset");

            DateTime? newDate = null;
            if (formValues.TryGetValue($"{controlId}-date", out var dateStr) && DateTime.TryParse(dateStr, out var currentDate))
            {
                if (prevPressed) newDate = currentDate.AddDays(-1);
                else if (nextPressed) newDate = currentDate.AddDays(1);
                else if (resetPressed && formValues.TryGetValue($"{controlId}-original-date", out var origStr) && DateTime.TryParse(origStr, out var origDate))
                    newDate = origDate;
                else newDate = currentDate;
            }

            return new DateNavControlResult
            {
                PrevDatePressed = prevPressed,
                NextDatePressed = nextPressed,
                RefreshPressed = refreshPressed,
                ResetPressed = resetPressed,
                NewSelectedDate = newDate
            };
        }
    }
}