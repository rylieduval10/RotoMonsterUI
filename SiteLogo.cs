using HtmlTags;

namespace RotoMonsterUI
{
    public class SiteLogo
    {
        private string _imgSrc;
        private string _homeUrl;
        private string _id;

        public SiteLogo(string imgSrc)
        {
            _imgSrc = imgSrc;
            _homeUrl = "/";
        }

        public SiteLogo WithHomeUrl(string url)
        {
            _homeUrl = url;
            return this;
        }

        public SiteLogo WithId(string id)
        {
            _id = id;
            return this;
        }

        public string Render()
        {
            var link = new HtmlTag("a")
                .AddClass("navbar-brand")
                .Attr("href", _homeUrl);

            if (!string.IsNullOrEmpty(_id))
                link.Attr("id", _id);

            var img = new HtmlTag("img")
                .Attr("src", _imgSrc)
                .Attr("alt", "Site Logo");

            link.Append(img);

            return link.ToString();
        }
    }
}