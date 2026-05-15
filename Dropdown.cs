using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class Dropdown
    {
        private string _label;
        private List<string> _items;
        private BootstrapVersion _version;

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

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("dropdown");

            var toggle = new HtmlTag("button")
                .AddClass("btn")
                .AddClass("btn-secondary")
                .AddClass("dropdown-toggle")
                .Attr("type", "button")
                .Attr("data-toggle", "dropdown")
                .Text(_label);

            var menu = new HtmlTag("div").AddClass("dropdown-menu");

            foreach (var item in _items)
            {
                var link = new HtmlTag("a")
                    .AddClass("dropdown-item")
                    .Attr("href", "#")
                    .Text(item);
                menu.Append(link);
            }

            wrapper.Append(toggle);
            wrapper.Append(menu);

            return wrapper.ToString();
        }
    }
}