using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CollapseControlService
    {
        public CollapseControlResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new CollapseControlResult();
            var key = $"{controlId}-toggle";

            if (formValues.ContainsKey(key))
                result.IsExpanded = formValues[key] == "1";

            return result;
        }
    }
}