using HtmlTags;

namespace RotoMonsterUI
{
    public class CollapseControl
    {
        private readonly CollapseControlInput _input;

        public CollapseControl(CollapseControlInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var contentId = $"{_input.Id}-content";

            // Chevron icon
            var chevronDirection = _input.IsExpanded ? "up" : "down";
            var chevronSvg = _input.IsExpanded
                ? @"<svg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2.5' stroke-linecap='round' stroke-linejoin='round'><polyline points='18 15 12 9 6 15'/></svg>"
                : @"<svg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2.5' stroke-linecap='round' stroke-linejoin='round'><polyline points='6 9 12 15 18 9'/></svg>";

            // Button
            var buttonClass = _input.ButtonStyle == ButtonStyle.Primary
                ? "modern-filter-btn modern-filter-btn-primary"
                : "modern-filter-btn modern-filter-btn-secondary";

            var button = new HtmlTag("button")
                .AddClass(buttonClass)
                .Attr("type", "button")
                .Attr("data-toggle", "collapse")
                .Attr("data-target", $"#{contentId}")
                .Attr("aria-expanded", _input.IsExpanded ? "true" : "false")
                .Attr("aria-controls", contentId)
                .Attr("name", $"{_input.Id}-toggle")
                .Attr("value", _input.IsExpanded ? "1" : "0");

            button.AppendHtml($"{_input.ButtonText}&nbsp;");
            button.AppendHtml(chevronSvg);

            // Collapsible content
            var contentDiv = new HtmlTag("div")
                .Attr("id", contentId)
                .AddClass(_input.IsExpanded ? "collapse show" : "collapse");

            contentDiv.AppendHtml(_input.CollapsibleHtml);

            // Wrapper
            var wrapper = new HtmlTag("div").Attr("id", _input.Id);
            wrapper.Append(button);
            wrapper.Append(contentDiv);

            return wrapper.ToString();
        }
    }
}