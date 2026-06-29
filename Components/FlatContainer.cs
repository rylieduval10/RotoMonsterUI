using HtmlTags;

namespace RotoMonsterUI
{
    public class FlatContainer
    {
        private string _title;
        private string _content;

        public FlatContainer WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public FlatContainer WithContent(string content)
        {
            _content = content;
            return this;
        }

        public string Render()
        {
            var container = new HtmlTag("div").AddClass("flat-container");

            if (!string.IsNullOrEmpty(_title))
            {
                var title = new HtmlTag("h3")
                    .AddClass("flat-container-title")
                    .Text(_title);
                container.Append(title);
            }

            if (!string.IsNullOrEmpty(_content))
            {
                var content = new HtmlTag("div")
                    .AddClass("flat-container-content")
                    .AppendHtml(_content);
                container.Append(content);
            }

            return container.ToString();
        }
    }
}