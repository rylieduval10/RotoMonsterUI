using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ToggleSwitchResult
    {
        public bool IsChecked { get; set; }
    }

    public class ToggleSwitchService
    {
        public ToggleSwitchResult Process(string name, Dictionary<string, string> formValues)
        {
            return new ToggleSwitchResult
            {
                IsChecked = formValues.ContainsKey(name) && formValues[name] == "1"
            };
        }
    }
}