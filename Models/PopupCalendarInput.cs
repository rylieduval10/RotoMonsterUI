using System;

namespace RotoMonsterUI
{
    public enum PopupCalendarMode { Single, Range }

    public class PopupCalendarInput
    {
        public string Id { get; set; }
        public PopupCalendarMode Mode { get; set; } = PopupCalendarMode.Single;
        public DateTime DisplayMonth { get; set; } = DateTime.Today;
        public DateTime? SelectedDate { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public string TriggerLabel { get; set; } = "Select Date";
    }
}