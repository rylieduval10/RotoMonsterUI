using System;

namespace RotoMonsterUI
{
    public class DateNavControlResult
    {
        public bool IsNewDateSelected { get; set; }
        public bool RefreshPressed { get; set; }
        public bool ResetPressed { get; set; }
        public DateTime? NewSelectedDate { get; set; }
    }
}