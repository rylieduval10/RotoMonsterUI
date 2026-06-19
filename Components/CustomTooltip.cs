using HtmlTags;
using System;

namespace RotoMonsterUI
{
    public class CustomTooltip
    {
        private string _triggerHtml;
        private string _contentHtml;
        private string _id;

        public CustomTooltip(string triggerHtml, string contentHtml)
        {
            _triggerHtml = triggerHtml;
            _contentHtml = contentHtml;
            _id = "bm-tip-" + Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        public string Render()
        {
            var wrapper = new HtmlTag("span")
                .AddClass("bm-tooltip-trigger")
                .Attr("data-bm-tooltip", _id)
                .AppendHtml(_triggerHtml);

            var content = new HtmlTag("div")
                .AddClass("bm-tooltip-content")
                .Attr("id", _id)
                .Attr("role", "tooltip")
                .AppendHtml(_contentHtml);

            var outer = new HtmlTag("span").AddClass("bm-tooltip-wrap");
            outer.Append(wrapper);
            outer.Append(content);

            return outer.ToString();
        }
    }
}