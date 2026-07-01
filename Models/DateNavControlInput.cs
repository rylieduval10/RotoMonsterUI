using System;

namespace RotoMonsterUI
{
    public class DateNavControlInput
    {
        public string Id { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Today;
        public DateTime OriginalDate { get; set; } = DateTime.Today;
        public bool ShowRefresh { get; set; } = true;
        public bool ShowDayOfWeek { get; set; } = false;
        public bool ShowYear { get; set; } = false;
        public int GameCount { get; set; } = 0;
    }
}