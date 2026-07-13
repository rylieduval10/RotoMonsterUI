using HtmlTags;

namespace RotoMonsterUI
{
    public class PageHeader
    {
        private string _title;
        private string _subtitle;
        private string _id;
        private bool _isHomePage;

        public PageHeader(string title)
        {
            _title = title;
        }

        public PageHeader WithSubtitle(string subtitle)
        {
            _subtitle = subtitle;
            return this;
        }

        public PageHeader WithId(string id)
        {
            _id = id;
            return this;
        }

        public PageHeader WithIsHomePage(bool isHomePage = true)
        {
            _isHomePage = isHomePage;
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("page-header");

            if (!_isHomePage)
                wrapper.AddClass("page-header--compact");

            if (!string.IsNullOrEmpty(_id))
                wrapper.Attr("id", _id);

            var title = new HtmlTag("h1").AppendHtml(_title);
            wrapper.Append(title);

            if (!string.IsNullOrEmpty(_subtitle))
            {
                var subtitle = new HtmlTag("p").AddClass("subtitle").AppendHtml(_subtitle);
                wrapper.Append(subtitle);
            }

            return wrapper.ToString();
        }
    }
}