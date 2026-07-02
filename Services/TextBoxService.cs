using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class TextBoxValidationConfig
    {
        public TextBoxType Type { get; set; } = TextBoxType.Text;
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public int? MaxLength { get; set; }
    }

    public class TextBoxResult
    {
        public string Value { get; set; }
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
    }

    public class TextBoxService
    {
        public TextBoxResult Process(string name, Dictionary<string, string> formValues, TextBoxValidationConfig config = null)
        {
            var result = new TextBoxResult();
            config = config ?? new TextBoxValidationConfig();

            if (!formValues.ContainsKey(name))
            {
                result.Value = "";
                return result;
            }

            result.Value = formValues[name];

            if (config.Type == TextBoxType.Number)
            {
                if (!double.TryParse(result.Value, out var numValue))
                {
                    result.Success = false;
                    result.ErrorMessage = "Please enter a valid number.";
                    return result;
                }

                if (config.MinValue.HasValue && numValue < config.MinValue.Value)
                {
                    result.Success = false;
                    result.ErrorMessage = $"Value must be at least {config.MinValue.Value}.";
                    return result;
                }

                if (config.MaxValue.HasValue && numValue > config.MaxValue.Value)
                {
                    result.Success = false;
                    result.ErrorMessage = $"Value must be at most {config.MaxValue.Value}.";
                    return result;
                }
            }
            else
            {
                if (config.MaxLength.HasValue && result.Value.Length > config.MaxLength.Value)
                {
                    result.Success = false;
                    result.ErrorMessage = $"Must be {config.MaxLength.Value} characters or fewer.";
                    return result;
                }
            }

            return result;
        }
    }
}