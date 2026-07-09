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
        ExportExcel,
        Save,
        Calendar,
        PersonSimple,
        PersonAlert,
        PersonConfirmed,
        PersonArmsDown,
        PersonArmsUp,
        Injury,
        Illness,
        Rest,
        Personal,
        CoachsDecision,
        Dental,
        PossibleSuspension,
        Other,
        TradePending,
        Contract,
        InjuryMaintenance,
        Warning,
        Error
    }

    public class IconInput
    {
        public IconType Type { get; set; }
        public int Size { get; set; } = 20;
        public string Color { get; set; } = "currentColor";
        public string Fill { get; set; } = "none";
    }
}