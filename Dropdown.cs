using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class Dropdown
    {
        private string _label;
        private List<string> _items;
        private BootstrapVersion _version;
        private string _id;

        public Dropdown(string label, BootstrapVersion version = BootstrapVersion.V4)
        {
            _label = label;
            _items = new List<string>();
            _version = version;
        }

        public Dropdown AddItem(string item)
        {
            _items.Add(item);
            return this;
        }

        public Dropdown WithId(string id)
        {
            _id = id;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("bm-custom-select");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            var trigger = new HtmlTag("div")
                .AddClass("bm-custom-select-trigger")
                .Text(_label);

            var arrow = new HtmlTag("span")
                .AddClass("bm-custom-select-arrow");

            trigger.Append(arrow);

            var options = new HtmlTag("div").AddClass("bm-custom-select-options");

            foreach (var item in _items)
            {
                var option = new HtmlTag("div")
                    .AddClass("bm-custom-select-option")
                    .Text(item);
                options.Append(option);
            }

            wrapper.Append(trigger);
            wrapper.Append(options);

            return wrapper.ToString();
        }
    }
}