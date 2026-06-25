namespace RotoMonsterUI
{
    public class LineupPlayer
    {
        public int? BattingOrder { get; set; }
        public DisplayPlayerInput Player { get; set; } 
        public string Handedness { get; set; }
        public string Position { get; set; }
        public string PositionColor { get; set; }
        public bool IsStartingPitcher { get; set; }
    }
}