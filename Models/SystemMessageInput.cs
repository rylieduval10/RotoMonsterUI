namespace RotoMonsterUI
{
    public enum SystemMessageType
    {
        Info,
        Warning,
        Error, 
        Success
    }

    public class SystemMessageInput
    {
        public SystemMessageType Type { get; set; } = SystemMessageType.Info;

        public string Text { get; set; }

        public bool ShowIcon { get; set; } = true;
    }
}