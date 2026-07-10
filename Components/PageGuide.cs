using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PageGuide
    {
        private readonly string _id;
        private string _title;
        private string _purpose;
        private readonly List<PageGuideSection> _sections = new List<PageGuideSection>();
        private string _tourId;

        public PageGuide(string id)
        {
            _id = id;
        }

        public PageGuide WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public PageGuide WithPurpose(string purpose)
        {
            _purpose = purpose;
            return this;
        }
        public PageGuide AddSection(string title, string contentHtml, bool defaultExpanded = false)
        {
            _sections.Add(new PageGuideSection { Title = title, ContentHtml = contentHtml, DefaultExpanded = defaultExpanded });
            return this;
        }

        public PageGuide WithTourId(string tourId)
        {
            _tourId = tourId;
            return this;
        }

        private const string ChevronSvg = "<svg width=\"14\" height=\"14\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M6 9l6 6 6-6\"/></svg>";

        public string Render()
        {
            var trigger = new HtmlTag("button")
                .AddClass("bm-page-guide-trigger")
                .Attr("type", "button")
                .Attr("data-guide-target", _id);
            trigger.AppendHtml(new Icon(new IconInput { Type = IconType.Info, Size = 16 }).Render());
            trigger.AppendHtml("<span>Page guide</span>");

            var modal = new HtmlTag("div")
                .AddClass("bm-page-guide-modal")
                .Attr("id", _id)
                .Attr("style", "display:none;");

            var panel = new HtmlTag("div").AddClass("bm-page-guide-panel");

            var header = new HtmlTag("div").AddClass("bm-page-guide-header");
            var headerTop = new HtmlTag("div").AddClass("bm-page-guide-header-top");
            headerTop.Append(new HtmlTag("h3").Text(_title ?? ""));
            var closeBtn = new HtmlTag("button")
                .AddClass("bm-page-guide-close")
                .Attr("type", "button")
                .Attr("aria-label", "Close")
                .Text("\u00d7");
            headerTop.Append(closeBtn);
            header.Append(headerTop);

            if (!string.IsNullOrEmpty(_purpose))
                header.Append(new HtmlTag("p").AddClass("bm-page-guide-purpose").Text(_purpose));

            panel.Append(header);

            var body = new HtmlTag("div").AddClass("bm-page-guide-body");
            foreach (var section in _sections)
            {
                var sectionDiv = new HtmlTag("div").AddClass("bm-page-guide-section");
                if (section.DefaultExpanded)
                    sectionDiv.AddClass("bm-page-guide-section--open");

                var sectionHeader = new HtmlTag("div").AddClass("bm-page-guide-section-header");
                sectionHeader.Append(new HtmlTag("span").Text(section.Title));
                sectionHeader.AppendHtml(ChevronSvg);
                sectionDiv.Append(sectionHeader);

                var sectionContent = new HtmlTag("div")
                    .AddClass("bm-page-guide-section-content")
                    .AppendHtml(section.ContentHtml ?? "");
                sectionDiv.Append(sectionContent);

                body.Append(sectionDiv);
            }
            panel.Append(body);

            var footer = new HtmlTag("div").AddClass("bm-page-guide-footer");
            var closeFooterBtn = new HtmlTag("button")
                .AddClass("bm-page-guide-close-btn")
                .Attr("type", "button")
                .Text("Close");
            footer.Append(closeFooterBtn);

            if (!string.IsNullOrEmpty(_tourId))
            {
                var tourBtn = new HtmlTag("button")
                    .AddClass("bm-page-guide-tour-btn")
                    .Attr("type", "button")
                    .Attr("data-start-tour", _tourId)
                    .Text("Start interactive tour");
                footer.Append(tourBtn);
            }
            panel.Append(footer);

            modal.Append(panel);

            return trigger.ToString() + modal.ToString();
        }
    }
}