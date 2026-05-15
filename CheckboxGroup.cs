using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CheckboxGroup
    {
        private List<(string label, string value, bool checked_)> _options;
        private string _id;

        public CheckboxGroup()
        {
            _options = new List<(string, string, bool)>();
        }

        public CheckboxGroup WithId(string id)
        {
            _id = id;
            return this;
        }

        public CheckboxGroup AddOption(string label, string value, bool isChecked = false)
        {
            _options.Add((label, value, isChecked));
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("adv-controls");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            foreach (var option in _options)
            {
                var inputId = $"chk-{option.value}";

                var input = new HtmlTag("input")
                    .Attr("type", "checkbox")
                    .Attr("id", inputId)
                    .Attr("value", option.value);

                if (option.checked_)
                    input.Attr("checked", "checked");

                var label = new HtmlTag("label")
                    .Attr("for", inputId)
                    .Text(option.label);

                wrapper.Append(input);
                wrapper.Append(label);
            }

            return wrapper.ToString();
        }
    }
}