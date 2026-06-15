using HtmlTags;

namespace RotoMonsterUI
{
    public class IconButton
    {
        private string _text;
        private string _icon;
        private string _url;
        private ButtonStyle _style;
        private string _id;
        private string _name;

        public IconInput IconInput { get; set; }

        public static string FontAwesome(string classes)
        {
            return $"<i class=\"{classes}\"></i>";
        }

        // Original constructor
        public IconButton(string text, string icon)
        {
            _text = text;
            _icon = icon;
            _style = ButtonStyle.Secondary;
        }

        // New constructor taking IconType
        public IconButton(string text, IconType iconType)
        {
            _text = text;
            _style = ButtonStyle.Secondary;
            IconInput = new IconInput { Type = iconType };
            _icon = new Icon(IconInput).Render();
        }

        public IconButton WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public IconButton WithStyle(ButtonStyle style)
        {
            _style = style;
            return this;
        }

        public IconButton WithId(string id)
        {
            _id = id;
            return this;
        }

        public IconButton WithTitle(string title)
        {
            _text = title;
            return this;
        }

        public IconButton WithName(string name)
        {
            _name = name;
            return this;
        }

        public string Render()
        {
            // Re-render icon in case IconInput was modified after construction
            if (IconInput != null)
                _icon = new Icon(IconInput).Render();

            var tag = string.IsNullOrEmpty(_url)
                ? new HtmlTag("button")
                : new HtmlTag("a").Attr("href", _url);

            tag.AddClass("modern-filter-btn");

            switch (_style)
            {
                case ButtonStyle.Primary:
                    tag.AddClass("modern-filter-btn-primary");
                    break;
                case ButtonStyle.Secondary:
                    tag.AddClass("modern-filter-btn-secondary");
                    break;
                default:
                    tag.AddClass($"btn btn-{_style.ToString().ToLower()}");
                    break;
            }

            if (!string.IsNullOrEmpty(_id))
                tag.Attr("id", _id);

            if (!string.IsNullOrEmpty(_name))
                tag.Attr("name", _name);

            tag.AppendHtml($"<span style='margin-right:0.35rem;'>{_icon}</span>{_text}");

            return tag.ToString();
        }
    }
}