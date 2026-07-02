using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public enum ScheduleGridColorType
    {
        MaxWeeks,
        Ease
    }

    public enum ScheduleGridSortBy
    {
        Team,
        Games,
        MaxWeeks,
        Ease
    }

    public class ScheduleGridDay
    {
        public DateTime Date { get; set; }
        public string Opponent { get; set; }
        public string EaseColor { get; set; }
        public bool IsQualityGame { get; set; }
        public bool IsAwayGame { get; set; }
    }

    public class ScheduleGridPeriodCell
    {
        public int Games { get; set; }
        public bool IsMaxWeek { get; set; }
        public string EaseColor { get; set; }
        public List<ScheduleGridDay> Days { get; set; } = new List<ScheduleGridDay>();
    }

    public class ScheduleGridPeriod
    {
        public int PeriodNumber { get; set; }
        public DateTime StartDate { get; set; }
        public int NumWeeks { get; set; } = 1;
    }

    public class ScheduleGridTeamSummary
    {
        public int Games { get; set; }
        public int MaxWeeks { get; set; }
        public int QualityGames { get; set; }
        public double AvgEase { get; set; }
        public string AvgEaseColor { get; set; }
    }

    public class ScheduleGridTeam
    {
        public string TeamCode { get; set; }
        public string TeamColor { get; set; }
        public Dictionary<int, ScheduleGridPeriodCell> Periods { get; set; } = new Dictionary<int, ScheduleGridPeriodCell>();
        public ScheduleGridTeamSummary Summary { get; set; } = new ScheduleGridTeamSummary();
    }

    public class ScheduleGridInput
    {
        public string Id { get; set; } = "schedule-grid";
        public List<ScheduleGridPeriod> Periods { get; set; } = new List<ScheduleGridPeriod>();
        public List<ScheduleGridTeam> Teams { get; set; } = new List<ScheduleGridTeam>();
        public DateTime? SelectedDate { get; set; }
        public int StartSelectedPeriod { get; set; }
        public int EndSelectedPeriod { get; set; }
        public ScheduleGridColorType ColorType { get; set; } = ScheduleGridColorType.MaxWeeks;
        public ScheduleGridSortBy SortBy { get; set; } = ScheduleGridSortBy.Team;
        public bool UseQualityGames { get; set; } = false;
        public bool ShowEasePositionFilter { get; set; } = false;
        public List<(string Text, string Value)> EasePositionOptions { get; set; } = new List<(string, string)>();
        public string EasePositionFilterValue { get; set; }
        public int? ExpandedPeriodNumber { get; set; }
    }
}