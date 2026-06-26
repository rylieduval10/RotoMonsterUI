using System;

namespace RotoMonsterUI
{
    public class PopupCalendarInput
    {
        public string Id { get; set; }
        public DateTime? SelectedDate { get; set; }
        public DateTime DisplayMonth { get; set; } = DateTime.Today;
        public string TriggerLabel { get; set; } = "Select a date";
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}