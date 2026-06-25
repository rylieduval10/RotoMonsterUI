using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class DateNavControl
    {
        private DateNavControlInput _input;

        public DateNavControl(DateNavControlInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("date-nav-control").Attr("id", _input.Id);

            if (_input.ShowRefresh)
            {
                var refresh = new HtmlTag("button")
                    .AddClass("date-nav-btn")
                    .Attr("name", $"{_input.Id}-refresh")
                    .Attr("value", "1")
                    .Text("Refresh");
                wrapper.Append(refresh);
            }

            var prevIcon = new Icon(new IconInput { Type = IconType.Previous, Size = 16 }).Render();
            var prev = new HtmlTag("button")
                .AddClass("date-nav-btn")
                .Attr("name", $"{_input.Id}-prev")
                .Attr("value", "1")
                .AppendHtml(prevIcon);
            wrapper.Append(prev);

            var dateFormat = _input.ShowDayOfWeek
                ? (_input.ShowYear ? "dddd, MMMM d, yyyy" : "dddd, MMMM d")
                : (_input.ShowYear ? "MMMM d, yyyy" : "MMMM d");

            var dateLabel = new HtmlTag("span")
                .AddClass("date-nav-label")
                .Text(_input.SelectedDate.ToString(dateFormat));
            wrapper.Append(dateLabel);

            if (_input.GameCount > 0)
            {
                var gameCount = new HtmlTag("span")
                    .AddClass("date-nav-game-count")
                    .Text($"{_input.GameCount} Games");
                wrapper.Append(gameCount);
            }

            var nextIcon = new Icon(new IconInput { Type = IconType.Next, Size = 16 }).Render();
            var next = new HtmlTag("button")
                .AddClass("date-nav-btn")
                .Attr("name", $"{_input.Id}-next")
                .Attr("value", "1")
                .AppendHtml(nextIcon);
            wrapper.Append(next);

            var hiddenDate = new HtmlTag("input")
                .Attr("type", "hidden")
                .Attr("name", $"{_input.Id}-date")
                .Attr("value", _input.SelectedDate.ToString("yyyy-MM-dd"));
            wrapper.Append(hiddenDate);

            return wrapper.ToString();
        }
    }
}