using HtmlTags;

namespace RotoMonsterUI
{
    public class UserMenu
    {
        private readonly UserMenuInput _input;

        public UserMenu(UserMenuInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var wrapper = new HtmlTag("div").AddClass("dropdown user-menu");

            var toggle = new HtmlTag("button")
                .AddClass("nav-link user-menu-toggle")
                .Attr("type", "button")
                .Attr("data-toggle", "dropdown")
                .Attr("aria-haspopup", "true")
                .Attr("aria-expanded", "false");

            toggle.Append(new HtmlTag("span").Text(_input.Username));
            toggle.AppendHtml(new Icon(new IconInput { Type = IconType.Kebab, Size = 16 }).Render());

            var menu = new HtmlTag("div").AddClass("dropdown-menu dropdown-menu-right");

            menu.Append(new HtmlTag("a").AddClass("dropdown-item").Attr("href", _input.SettingsUrl).Text("Settings"));
            menu.Append(new HtmlTag("a").AddClass("dropdown-item").Attr("href", _input.MailUrl).Text("Mail"));
            menu.Append(new HtmlTag("div").AddClass("dropdown-divider"));
            menu.Append(new HtmlTag("a").AddClass("dropdown-item").Attr("href", _input.LogoutUrl).Text("Logout"));

            wrapper.Append(toggle);
            wrapper.Append(menu);

            return wrapper.ToString();
        }
    }
}