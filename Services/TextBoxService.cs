using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class TextBoxResult
    {
        public string Value { get; set; }
    }

    public class TextBoxService
    {
        public TextBoxResult Process(string name, Dictionary<string, string> formValues)
        {
            var result = new TextBoxResult();

            if (formValues.ContainsKey(name))
                result.Value = formValues[name];

            return result;
        }
    }
}