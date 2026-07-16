using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class TimeSinceBadge
    {
        private readonly TimeSpan _timeSpan;

        public TimeSinceBadge(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public string Render()
        {
            var ageColor = ColorHelper.GetAgeShadeHex(_timeSpan);
            var badge = new HtmlTag("span").AddClass("time-since-badge");
            if (ageColor != null)
                badge.Attr("style", $"background:{ageColor};");
            else
                badge.AddClass("time-since-badge--expired");
            badge.AppendHtml(new TimeSince(_timeSpan).Render());
            return badge.ToString();
        }
    }
}