using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CheckboxGroupResult
    {
        public List<string> SelectedValues { get; set; } = new List<string>();
    }

    public class CheckboxGroupService
    {
        public CheckboxGroupResult Process(string controlId, List<string> values, Dictionary<string, string> formValues)
        {
            var result = new CheckboxGroupResult();

            foreach (var value in values)
            {
                var key = $"{controlId}{value}";
                if (formValues.ContainsKey(key))
                    result.SelectedValues.Add(value);
            }

            return result;
        }
    }
}