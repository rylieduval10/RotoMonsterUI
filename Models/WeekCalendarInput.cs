using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class WeekCalendarWeek
    {
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public int GameCount { get; set; }
        public bool IsCurrentWeek { get; set; }
    }

    public class WeekCalendarInput
    {
        public List<WeekCalendarWeek> Weeks { get; set; } = new List<WeekCalendarWeek>();
    }
}