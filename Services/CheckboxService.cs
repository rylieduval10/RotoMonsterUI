using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CheckboxResult
    {
        public bool IsChecked { get; set; }
    }

    public class CheckboxService
    {
        public CheckboxResult Process(string name, Dictionary<string, string> formValues)
        {
            return new CheckboxResult
            {
                IsChecked = formValues.ContainsKey(name) && formValues[name] == "1"
            };
        }
    }
}