using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ScheduleTeamRowInput
    {
        public string Id { get; set; } = "schedule-team-row";
        public string TeamCode { get; set; }

        public List<ScheduleGridPeriod> Periods { get; set; } = new List<ScheduleGridPeriod>();

        // Key = PeriodNumber
        public Dictionary<int, ScheduleGridPeriodCell> PeriodCells { get; set; } = new Dictionary<int, ScheduleGridPeriodCell>();

        public ScheduleGridColorType ColorType { get; set; } = ScheduleGridColorType.MaxWeeks;
        public int? CurrentPeriodNumber { get; set; }

        // If set, this period's row shows an expanded breakdown of each day's opponent/ease below it
        public int? ExpandedPeriodNumber { get; set; }
    }
}