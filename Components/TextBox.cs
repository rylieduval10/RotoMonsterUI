using HtmlTags;

namespace RotoMonsterUI
{
    public class TextBox
    {
        private string _name;
        private string _id;
        private string _value;
        private string _placeholder;
        private bool _postBack;

        public string Id => _id ?? _name ?? "";

        public TextBox WithName(string name)
        {
            _name = name;
            return this;
        }

        public TextBox WithId(string id)
        {
            _id = id;
            return this;
        }

        public TextBox WithValue(string value)
        {
            _value = value;
            return this;
        }

        public TextBox WithPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }

        public TextBox WithPostBack()
        {
            _postBack = true;
            return this;
        }

        public string Render()
        {
            var input = new HtmlTag("input")
                .Attr("type", "text")
                .AddClass("bm-textbox")
                .Attr("name", _name ?? "")
                .Attr("id", _id ?? _name ?? "");

            if (!string.IsNullOrEmpty(_placeholder))
                input.Attr("placeholder", _placeholder);

            if (!string.IsNullOrEmpty(_value))
                input.Attr("value", _value);

            if (_postBack && !string.IsNullOrEmpty(_name))
            {
                input.Attr("onchange", $"__doPostBack('{_name}','')");
                input.Attr("language", "javascript");
            }

            return input.ToString();
        }
    }
}