namespace RotoMonsterUI
{
    public class LineupCardInput
    {
        public string Id { get; set; }
        public LineupCardTeamInput AwayTeam { get; set; }
        public LineupCardTeamInput HomeTeam { get; set; }
        public GameInput Game { get; set; }
        public bool IsDarkMode { get; set; } = false;
    }
}