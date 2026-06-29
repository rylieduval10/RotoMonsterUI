using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class TextAreaResult
    {
        public string Value { get; set; }
    }

    public class TextAreaService
    {
        public TextAreaResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new TextAreaResult();

            if (formValues.TryGetValue(controlId, out var value))
                result.Value = value;

            return result;
        }
    }
}