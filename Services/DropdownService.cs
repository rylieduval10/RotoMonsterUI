using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DropdownResult
    {
        public string SelectedValue { get; set; }
    }

    public class DropdownService
    {
        public DropdownResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new DropdownResult();

            if (formValues.ContainsKey(controlId))
                result.SelectedValue = formValues[controlId];

            return result;
        }
    }
}