using HtmlTags;

namespace RotoMonsterUI
{
    public class ToggleSwitch
    {
        private string _label;
        private string _name;
        private bool _checked;
        private string _id;

        public ToggleSwitch WithLabel(string label)
        {
            _label = label;
            return this;
        }

        public ToggleSwitch WithName(string name)
        {
            _name = name;
            return this;
        }

        public ToggleSwitch WithId(string id)
        {
            _id = id;
            return this;
        }

        public ToggleSwitch WithChecked(bool isChecked)
        {
            _checked = isChecked;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("label").AddClass("modern-filter-toggle");

            var input = new HtmlTag("input")
                .Attr("type", "checkbox")
                .Attr("name", _name ?? "")
                .Attr("id", _id ?? _name ?? "");

            if (_checked)
                input.Attr("checked", "checked");

            var switchDiv = new HtmlTag("div").AddClass("modern-filter-toggle-switch");

            var labelSpan = new HtmlTag("span")
                .AddClass("modern-filter-toggle-label")
                .AppendHtml(_label ?? "");

            wrapper.Append(input);
            wrapper.Append(switchDiv);
            wrapper.Append(labelSpan);

            return wrapper.ToString();
        }
    }
}