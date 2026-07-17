using HtmlTags;

namespace RotoMonsterUI
{
    public class SearchBox
    {
        private string _placeholder;
        private string _name;
        private string _id;
        private string _value;

        public SearchBox()
        {
            _placeholder = "Search...";
        }

        public SearchBox WithPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }

        public SearchBox WithName(string name)
        {
            _name = name;
            return this;
        }

        public SearchBox WithId(string id)
        {
            _id = id;
            return this;
        }

        public SearchBox WithValue(string value)
        {
            _value = value;
            return this;
        }

        public string Render()
        {
            var input = new HtmlTag("input")
                .Attr("type", "text")
                .AddClass("form-control")
                .Attr("placeholder", _placeholder)
                .Attr("autocomplete", "off");

            if (!string.IsNullOrEmpty(_name))
                input.Attr("name", _name);

            if (!string.IsNullOrEmpty(_id))
                input.Attr("id", _id);

            if (!string.IsNullOrEmpty(_value))
                input.Attr("value", _value);

            return input.ToString();
        }
    }
}