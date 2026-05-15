using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public enum BootstrapVersion
    {
        V4,
        V5
    }

    public enum ButtonStyle
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Info,
        Light,
        Dark
    }

    public class Button
    {
        private string _text;
        private ButtonStyle _style;
        private BootstrapVersion _version;
        private string _id;

        public Button(string text, BootstrapVersion version = BootstrapVersion.V4)
        {
            _text = text;
            _style = ButtonStyle.Primary;
            _version = version;
        }

        public Button WithStyle(ButtonStyle style)
        {
            _style = style;
            return this;
        }

        public Button WithId(string id)
        {
            _id = id;
            return this;
        }

        public string Render()
        {
            var tag = new HtmlTag("button").AddClass("modern-filter-btn");

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

            return tag.Text(_text).ToString();
        }
    }
}