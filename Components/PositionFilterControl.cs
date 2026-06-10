using System.Collections.Generic;
using HtmlTags;

namespace RotoMonsterUI
{
    public class PositionFilterControl
    {
        private PositionFilterControlInput _input;

        public PositionFilterControl(PositionFilterControlInput input)
        {
            _input = input;
        }

        private string NormalizeColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return color;
            return color.StartsWith("#") ? color : "#" + color;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div")
                .AddClass("modern-filter-badges")
                .Attr("id", _input.Id);

            // All button
            if (!string.IsNullOrEmpty(_input.AllButtonId))
            {
                var allBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("id", _input.AllButtonId)
                    .Attr("name", _input.AllButtonId)
                    .Text("All");
                wrapper.Append(allBtn);
            }

            // Clear button
            if (!string.IsNullOrEmpty(_input.ClearButtonId))
            {
                var clearBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("id", _input.ClearButtonId)
                    .Attr("name", _input.ClearButtonId)
                    .Text("Clear");
                wrapper.Append(clearBtn);
            }

            // Position badges
            foreach (var pos in _input.Positions)
            {
                var isSelected = _input.SelectedPositionIds.Contains(pos.Id);

                var badge = new HtmlTag("button")
                    .AddClass("modern-filter-badge")
                    .Attr("data-pos", pos.Abbreviation)
                    .Attr("data-pos-id", pos.Id.ToString())
                    .Attr("name", $"pos_{pos.Id}")
                    .Attr("style", $"color:{NormalizeColor(pos.Color)}");

                if (isSelected)
                    badge.AddClass("active");

                badge.Text(pos.Abbreviation);
                wrapper.Append(badge);
            }

            return wrapper.ToString();
        }
    }
}