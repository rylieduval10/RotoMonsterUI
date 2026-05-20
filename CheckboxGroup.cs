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
                var input = new HtmlTag("input")
                    .Attr("type", "checkbox")
                    .Attr("id", $"{_id}{option.value}")
                    .Attr("name", $"{_id}{option.value}");

                if (option.checked_)
                    input.Attr("checked", "checked");

                var label = new HtmlTag("label")
                    .Attr("for", $"{_id}{option.value}")
                    .AddClass("modern-filter-badge")
                    .Text(option.label);

                if (!string.IsNullOrEmpty(option.dataPos))
                    label.Attr("data-pos", option.dataPos);

                if (option.checked_)
                    label.AddClass("active");

                wrapper.Append(input);
                wrapper.Append(label);
            }

            return wrapper.ToString();
        }
    }
}