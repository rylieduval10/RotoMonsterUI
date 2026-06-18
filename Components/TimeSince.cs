using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class TimeSince
    {
        private TimeSpan _timeSpan;

        public TimeSince(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public string Render()
        {
            string display;

            if (_timeSpan.TotalSeconds < 60)
                display = $"{(int)_timeSpan.TotalSeconds}s";
            else if (_timeSpan.TotalMinutes < 60)
                display = $"{(int)_timeSpan.TotalMinutes}m";
            else if (_timeSpan.TotalHours < 24)
                display = $"{Math.Round(_timeSpan.TotalHours, 1)}h";
            else
                display = new DisplayDate(new DisplayDateInput { Date = DateTime.Now.Subtract(_timeSpan) }).Render();

            var tag = new HtmlTag("span")
                .AddClass("time-since")
                .Text(display);

            return tag.ToString();
        }
    }
}