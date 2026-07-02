using HtmlTags;
using System.Collections.Generic;
using System.Text.Json;

namespace RotoMonsterUI
{
    public class Tour
    {
        private readonly string _id;
        private readonly List<TourStep> _steps = new List<TourStep>();

        public Tour(string id)
        {
            _id = id;
        }

        public Tour AddStep(string targetId, string text, TooltipPosition position = TooltipPosition.Bottom)
        {
            _steps.Add(new TourStep { TargetId = targetId, Text = text, Position = position });
            return this;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div")
                .AddClass("bm-tour")
                .Attr("id", _id)
                .Attr("style", "display:none;");

            var stepsJson = JsonSerializer.Serialize(_steps);
            wrapper.Attr("data-tour-steps", stepsJson);

            return wrapper.ToString();
        }
    }
}