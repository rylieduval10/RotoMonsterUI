namespace RotoMonsterUI
{
    public class UserMenuInput
    {
        public string Username { get; set; }
        public string SettingsUrl { get; set; } = "/settings";
        public string MailUrl { get; set; } = "/mail";
        public string LogoutUrl { get; set; } = "/logout";
    }
}