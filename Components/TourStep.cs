namespace RotoMonsterUI
{
    public enum TooltipPosition
    {
        Bottom,
        Top,
        Left,
        Right
    }

    public class TourStep
    {
        public string TargetId { get; set; }
        public string Text { get; set; }
        public TooltipPosition Position { get; set; } = TooltipPosition.Bottom;
    }
}