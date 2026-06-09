using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayUsername
    {
        private DisplayUsernameInput _input;

        public DisplayUsername(DisplayUsernameInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var displayText = !string.IsNullOrEmpty(_input.Username)
                ? _input.Username
                : _input.UserId.HasValue ? $"#{_input.UserId}" : "";

            var tag = new HtmlTag("span")
                .AddClass("display-username");

            if (!string.IsNullOrEmpty(_input.CssClass))
                tag.AddClass(_input.CssClass);

            tag.Text(displayText);

            return tag.ToString();
        }
    }
}