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
                    .AddClass("modern-filter-btn modern-filter-btn-secondary pos-filter-all")
                    .Attr("id", allId)
                    .Attr("name", allId)
                    .Attr("value", "1")
                    .Text("All");
                wrapper.Append(allBtn);
            }

            if (_input.ShowClearButton)
            {
                var clearId = $"{_input.Id}-clear";
                var clearBtn = new HtmlTag("button")
                    .AddClass("modern-filter-btn modern-filter-btn-secondary pos-filter-clear")
                    .Attr("id", clearId)
                    .Attr("name", clearId)
                    .Attr("value", "1")
                    .Text("Clear");
                wrapper.Append(clearBtn);
            }

            foreach (var pos in _input.Positions)
            {
                var isSelected = _input.SelectedPositionIds.Contains(pos.Id);
                var checkboxId = $"{_input.Id}_pos_{pos.Id}";

                var label = new HtmlTag("label")
                    .AddClass("badge-checkbox")
                    .Attr("for", checkboxId);

                var checkbox = new HtmlTag("input")
                    .Attr("type", "checkbox")
                    .Attr("id", checkboxId)
                    .Attr("name", $"pos_{pos.Id}")
                    .Attr("value", pos.Id.ToString());

                if (isSelected)
                    checkbox.Attr("checked", "checked");

                var badgeLabel = new HtmlTag("span")
                    .AddClass("badge-label modern-filter-badge")
                    .Attr("data-pos", pos.Abbreviation)
                    .Attr("style", $"--pos-color:{NormalizeColor(pos.Color)};color:{NormalizeColor(pos.Color)};")
                    .Text(pos.Abbreviation);

                label.Append(checkbox);
                label.Append(badgeLabel);
                wrapper.Append(label);
            }

            return wrapper.ToString();
        }
    }
}