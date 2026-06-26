using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class RichTextEditorService
    {
        public RichTextEditorResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new RichTextEditorResult();

            if (formValues.TryGetValue(controlId, out var value))
                result.HtmlValue = value;

            return result;
        }
    }
}