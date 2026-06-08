using HtmlTags;

namespace RotoMonsterUI
{
    public class NextGamesTable
    {
        private string _tableHtml;
        private string _title;

        public NextGamesTable(string tableHtml)
        {
            _tableHtml = tableHtml;
        }

        public NextGamesTable WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("next-games-wrapper");

            if (!string.IsNullOrEmpty(_title))
            {
                var title = new HtmlTag("h2")
                    .AddClass("next-games-title")
                    .Text(_title);
                wrapper.Append(title);
            }

            var tableWrapper = new HtmlTag("div")
                .AddClass("next-games-table-wrapper")
                .AppendHtml(_tableHtml);

            wrapper.Append(tableWrapper);

            return wrapper.ToString();
        }
    }
}