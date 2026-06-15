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

            if (_input.ShowAllButton)
            {
                var allId = $"{_input.Id}-all";
                var allBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("id", allId)
                    .Attr("name", allId)
                    .Text("All");
                wrapper.Append(allBtn);
            }

            if (_input.ShowClearButton)
            {
                var clearId = $"{_input.Id}-clear";
                var clearBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary")
                    .Attr("id", clearId)
                    .Attr("name", clearId)
                    .Text("Clear");
                wrapper.Append(clearBtn);
            }

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