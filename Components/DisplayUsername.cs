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

        public string RenderAvatar()
        {
            var displayText = !string.IsNullOrEmpty(_input.Username)
                ? _input.Username
                : _input.UserId.HasValue ? $"#{_input.UserId}" : "";

            if (!string.IsNullOrEmpty(_input.AvatarUrl))
            {
                var img = new HtmlTag("img")
                    .AddClass("display-username-avatar")
                    .Attr("src", _input.AvatarUrl)
                    .Attr("alt", displayText);
                return img.ToString();
            }

            var initial = !string.IsNullOrEmpty(displayText) ? displayText.Substring(0, 1).ToUpper() : "?";
            var fallback = new HtmlTag("span")
                .AddClass("display-username-avatar display-username-avatar--fallback")
                .Text(initial);
            return fallback.ToString();
        }

        public string Render()
        {
            var displayText = !string.IsNullOrEmpty(_input.Username)
                ? _input.Username
                : _input.UserId.HasValue ? $"#{_input.UserId}" : "";

            var wrapper = new HtmlTag("span").AddClass("display-username-wrap");

            if (_input.ShowAvatar)
                wrapper.AppendHtml(RenderAvatar());

            var tag = new HtmlTag("span")
                .AddClass("display-username");

            if (!string.IsNullOrEmpty(_input.CssClass))
                tag.AddClass(_input.CssClass);
            else
                tag.AddClass("display-username--default-color");

            tag.Text(displayText);

            if (!_input.ShowAvatar && !_input.TotalPostCount.HasValue)
                return tag.ToString();

            wrapper.Append(tag);

            if (_input.TotalPostCount.HasValue)
            {
                var postCount = new HtmlTag("span")
                    .AddClass("display-username-postcount")
                    .Text($"({_input.TotalPostCount.Value} posts)");
                wrapper.Append(postCount);
            }

            return wrapper.ToString();
        }
    }
}