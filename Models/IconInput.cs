namespace RotoMonsterUI
{
    public enum IconType
    {
        Settings,
        RefreshRosters,
        PostponementChanceWarning,
        Info,
        Dome,
        RetractableDome,
        Rain,
        Trash,
        Next,
        Previous,
        LineupConfirmed,
        LineupNotConfirmed,
        Weather,
        LineupCard,
        ExportCSV,
        ExportExcel
    }

    public class IconInput
    {
        public IconType Type { get; set; }
        public int Size { get; set; } = 24;
        public string Color { get; set; } = "currentColor";
        public string Fill { get; set; } = "none";
    }
}