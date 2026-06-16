using System;

namespace RotoMonsterUI
{
    public class CalendarPickerResult
    {
        public DateTime? SelectedDate { get; set; }
        public bool PrevMonthPressed { get; set; }
        public bool NextMonthPressed { get; set; }
    }
}