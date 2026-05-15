using System;
using System.Diagnostics;
using HtmlTags;

namespace RotoMonsterUI
{
    //saying that bootstrap can either be version 4 or version 5
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

        public string Render()
        {
            string styleClass = _style.ToString().ToLower();
            return new HtmlTag("button")
                .AddClass("btn")
                .AddClass($"btn-{styleClass}")
                .Text(_text)
                .ToString();
        }
    }
}
