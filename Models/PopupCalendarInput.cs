using System;

namespace RotoMonsterUI
{
    public enum CalendarPickerMode
    {
        Single,
        Range
    }

    public class PopupCalendarInput
    {
        public string Id { get; set; }
        public CalendarPickerMode Mode { get; set; } = CalendarPickerMode.Single;
        public DateTime? SelectedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DisplayMonth { get; set; } = DateTime.Today;
        public string TriggerLabel { get; set; } = "Select a date";
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}