using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CheckboxGroup
    {
        private List<(string label, string value, bool checked_, string dataPos)> _options;
        private string _id;
        private string _name;

        public CheckboxGroup()
        {
            _options = new List<(string, string, bool, string)>();
        }

        public CheckboxGroup WithId(string id)
        {
            _id = id;
            return this;
        }

        public CheckboxGroup WithName(string name)
        {
            _name = name;
            return this;
        }

        public CheckboxGroup AddOption(string label, string value, bool isChecked = false, string dataPos = null)
        {
            _options.Add((label, value, isChecked, dataPos));
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("modern-filter-badges");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            foreach (var option in _options)
            {
                // REMOVED: var container = new HtmlTag("div").AddClass("modern-filter-badge-wrapper");

                var input = new HtmlTag("input")
                    .Attr("type", "checkbox")
                    .Attr("value", option.value)
                    .Attr("id", $"{_name}_{option.value}");

                if (!string.IsNullOrEmpty(_name))
                    input.Attr("name", _name);

                if (!string.IsNullOrEmpty(option.dataPos))
                    input.Attr("data-pos", option.dataPos);

                if (option.checked_)
                    input.Attr("checked", "checked");

                var label = new HtmlTag("label")
                    .Attr("for", $"{_name}_{option.value}")
                    .AddClass("modern-filter-badge")
                    .Text(option.label);

                // Append directly to the top-level modern-filter-badges wrapper
                wrapper.Append(input);
                wrapper.Append(label);
            }

            return wrapper.ToString();
        }
    }
}