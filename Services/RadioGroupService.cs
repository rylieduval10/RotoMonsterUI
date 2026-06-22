using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class RadioGroupResult
    {
        public string SelectedValue { get; set; }
    }

    public class RadioGroupService
    {
        public RadioGroupResult Process(string name, Dictionary<string, string> formValues)
        {
            var result = new RadioGroupResult();

            if (formValues.ContainsKey(name))
                result.SelectedValue = formValues[name];

            return result;
        }
    }
}