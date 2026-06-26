using System;

namespace RotoMonsterUI
{
    public class PopupCalendarResult
    {
        public DateTime? SelectedDate { get; set; }
        public bool PrevMonthPressed { get; set; }
        public bool NextMonthPressed { get; set; }
        public DateTime? DisplayMonth { get; set; }
    }
}