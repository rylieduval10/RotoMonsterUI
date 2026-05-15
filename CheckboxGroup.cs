using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CheckboxGroup
    {
        private List<(string label, string value, bool checked_)> _options;
        private string _id;
        private string _name;

        public CheckboxGroup()
        {
            _options = new List<(string, string, bool)>();
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

        public CheckboxGroup AddOption(string label, string value, bool isChecked = false)
        {
            _options.Add((label, value, isChecked));
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("modern-filter-badges");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            foreach (var option in _options)
            {
                var badge = new HtmlTag("div")
                    .AddClass("modern-filter-badge")
                    .Attr("data-value", option.value);

                if (!string.IsNullOrEmpty(_name))
                    badge.Attr("data-name", _name);

                if (option.checked_)
                    badge.AddClass("active");

                var checkmark = new HtmlTag("span")
                    .Attr("class", "badge-checkmark")
                    .Attr("style", option.checked_ ? "visibility:visible" : "visibility:hidden")
                    .Text("✓ ");

                badge.Append(checkmark);
                badge.AppendHtml(option.label);

                wrapper.Append(badge);
            }

            return wrapper.ToString();
        }
    }
}