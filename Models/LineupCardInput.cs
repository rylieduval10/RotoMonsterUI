namespace RotoMonsterUI
{
    public class LineupCardInput
    {
        public string Id { get; set; }
        public LineupCardTeamInput AwayTeam { get; set; }
        public LineupCardTeamInput HomeTeam { get; set; }
        public DisplayGameInput Game { get; set; }
        public bool IsDarkMode { get; set; } = false;
    }
}