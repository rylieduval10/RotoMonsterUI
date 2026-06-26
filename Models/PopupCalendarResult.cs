using System;

namespace RotoMonsterUI
{
    public class PopupCalendarResult
    {
        public DateTime? SelectedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool PrevMonthPressed { get; set; }
        public bool NextMonthPressed { get; set; }
        public DateTime? DisplayMonth { get; set; }
    }
}