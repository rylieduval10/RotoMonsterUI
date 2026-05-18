using HtmlTags;

namespace RotoMonsterUI
{
    public class Card
    {
        private string _title;
        private string _content;

        public Card WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public Card WithContent(string content)
        {
            _content = content;
            return this;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("card-based");

            if (!string.IsNullOrEmpty(_title))
            {
                var title = new HtmlTag("h5")
                    .AddClass("card-title")
                    .Text(_title);
                card.Append(title);
            }

            if (!string.IsNullOrEmpty(_content))
            {
                var content = new HtmlTag("div")
                    .AddClass("card-content")
                    .AppendHtml(_content);
                card.Append(content);
            }

            return card.ToString();
        }
    }
}