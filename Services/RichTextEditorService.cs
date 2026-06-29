using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class RichTextEditorService
    {
        public RichTextEditorResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new RichTextEditorResult();

            if (formValues.TryGetValue(controlId, out var value) && !string.IsNullOrEmpty(value))
            {
                try
                {
                    var bytes = Convert.FromBase64String(value);
                    result.HtmlValue = System.Text.Encoding.UTF8.GetString(bytes);
                }
                catch
                {
                    result.HtmlValue = value;
                }
            }

            return result;
        }
    }
}