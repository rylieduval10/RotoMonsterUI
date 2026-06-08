using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class Dropdown
    {
        private string _label;
        private List<(string text, string value)> _items;
        private BootstrapVersion _version;
        private string _id;
        private string _selectedValue;
        private string _name;

        public Dropdown(string label, BootstrapVersion version = BootstrapVersion.V4)
        {
            _label = label;
            _items = new List<(string, string)>();
            _version = version;
        }

        public Dropdown AddItem(string text, string value = null)
        {
            _items.Add((text, value ?? text));
            return this;
        }

        public Dropdown WithId(string id)
        {
            _id = id;
            return this;
        }

        public Dropdown WithName(string name)
        {
            _name = name;
            return this;
        }

        public Dropdown WithSelectedValue(string value)
        {
            _selectedValue = value;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("bm-custom-select");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            if (!string.IsNullOrEmpty(_name))
                wrapper.Attr("data-name", _name);

            // Find selected text
            var selectedText = _label;
            foreach (var item in _items)
            {
                if (item.value == _selectedValue)
                {
                    selectedText = item.text;
                    break;
                }
            }

            // Trigger with value span
            var trigger = new HtmlTag("div").AddClass("bm-custom-select-trigger");
            var valueSpan = new HtmlTag("span").AddClass("bm-custom-select-value").Text(selectedText);
            var arrow = new HtmlTag("span").AddClass("bm-custom-select-arrow");
            trigger.Append(valueSpan);
            trigger.Append(arrow);

            // Options
            var options = new HtmlTag("div").AddClass("bm-custom-select-options");
            foreach (var item in _items)
            {
                var option = new HtmlTag("div")
                    .AddClass("bm-custom-select-option")
                    .Attr("data-value", item.value)
                    .Text(item.text);

                if (item.value == _selectedValue)
                    option.AddClass("selected");

                options.Append(option);
            }

            // Hidden select for postback
            var select = new HtmlTag("select");
            if (!string.IsNullOrEmpty(_name))
            {
                select.Attr("name", _name);
                select.Attr("id", _name);
                select.Attr("onchange", $"__doPostBack('{_name}','')");
                select.Attr("language", "javascript");
            }

            foreach (var item in _items)
            {
                var selectOption = new HtmlTag("option")
                    .Attr("value", item.value)
                    .Text(item.text);

                if (item.value == _selectedValue)
                    selectOption.Attr("selected", "selected");

                select.Append(selectOption);
            }

            wrapper.Append(trigger);
            wrapper.Append(options);
            wrapper.Append(select);

            return wrapper.ToString();
        }
    }
}