using HtmlTags;

namespace RotoMonsterUI
{
    public class SystemMessage
    {
        private readonly SystemMessageInput _input;

        public SystemMessage(SystemMessageInput input)
        {
            _input = input;
        }

        private static string ClassSuffixFor(SystemMessageType type)
        {
            switch (type)
            {
                case SystemMessageType.Warning: return "warning";
                case SystemMessageType.Error: return "error";
                default: return "info";
            }
        }

        private static IconType IconTypeFor(SystemMessageType type)
        {
            switch (type)
            {
                case SystemMessageType.Warning: return IconType.Warning;
                case SystemMessageType.Error: return IconType.Error;
                default: return IconType.Info;
            }
        }

        public string Render()
        {
            var suffix = ClassSuffixFor(_input.Type);

            var wrapper = new HtmlTag("div")
                .AddClass("system-message")
                .AddClass($"system-message--{suffix}");

            if (_input.ShowIcon)
            {
                var icon = new Icon(new IconInput { Type = IconTypeFor(_input.Type), Size = 18 }).Render();
                var iconWrap = new HtmlTag("span").AddClass("system-message-icon").AppendHtml(icon);
                wrapper.Append(iconWrap);
            }

            var text = new HtmlTag("span").AddClass("system-message-text").AppendHtml(_input.Text ?? "");
            wrapper.Append(text);

            return wrapper.ToString();
        }
    }
}