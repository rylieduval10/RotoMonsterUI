using System;

namespace RotoMonsterUI
{
    public class CalendarPickerInput
    {
        public string Id { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}