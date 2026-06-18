using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DateNavControlService
    {
        public DateNavControlResult Process(string controlId, Dictionary<string, string> formValues)
        {
            return new DateNavControlResult
            {
                PrevDatePressed = formValues.ContainsKey($"{controlId}-prev"),
                NextDatePressed = formValues.ContainsKey($"{controlId}-next"),
                RefreshPressed = formValues.ContainsKey($"{controlId}-refresh")
            };
        }
    }
}