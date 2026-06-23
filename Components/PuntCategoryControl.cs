using System.Collections.Generic;
using HtmlTags;

namespace RotoMonsterUI
{
    public class PuntCategoryControl
    {
        private readonly PuntCategoryInput _input;

        public PuntCategoryControl(PuntCategoryInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div")
                .AddClass("modern-filter-badges")
                .Attr("id", _input.Id);

            foreach (var category in _input.Categories)
            {
                var isSelected = _input.SelectedIds.Contains(category.Id);
                var checkboxId = $"{_input.Id}_cat_{category.Id}";

                var label = new HtmlTag("label")
                    .AddClass("badge-checkbox")
                    .Attr("for", checkboxId);

                var checkbox = new HtmlTag("input")
                    .Attr("type", "checkbox")
                    .Attr("id", checkboxId)
                    .Attr("name", $"cat_{category.Id}")
                    .Attr("value", category.Id.ToString());

                if (isSelected)
                    checkbox.Attr("checked", "checked");

                var badgeLabel = new HtmlTag("span")
                    .AddClass("badge-label modern-filter-badge")
                    .Attr("data-cat", category.Abbreviation)
                    .Attr("style", $"--cat-color:{category.ColorCSS};color:{category.ColorCSS};")
                    .Text(category.Abbreviation);

                label.Append(checkbox);
                label.Append(badgeLabel);
                wrapper.Append(label);
            }

            return wrapper.ToString();
        }
    }
}