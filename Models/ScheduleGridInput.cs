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

    // A single game/day within a period, used for the expand-row feature
    public class ScheduleGridDay
    {
        public DateTime Date { get; set; }
        public string Opponent { get; set; }
        public string EaseColor { get; set; }
        public bool IsQualityGame { get; set; }
    }

    // The intersection of one team + one period (a single grid cell)
    public class ScheduleGridPeriodCell
    {
        public int Games { get; set; }
        public bool IsMaxWeek { get; set; }
        public string EaseColor { get; set; }
        public List<ScheduleGridDay> Days { get; set; } = new List<ScheduleGridDay>();
    }

    // Row header info shared across all teams (Period #, Start Date, # Weeks)
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

        // Key = PeriodNumber
        public Dictionary<int, ScheduleGridPeriodCell> Periods { get; set; } = new Dictionary<int, ScheduleGridPeriodCell>();

        // Based on the currently selected Start/End period range
        public ScheduleGridTeamSummary Summary { get; set; } = new ScheduleGridTeamSummary();
    }

    public class ScheduleGridInput
    {
        public string Id { get; set; } = "schedule-grid";

        public List<ScheduleGridPeriod> Periods { get; set; } = new List<ScheduleGridPeriod>();
        public List<ScheduleGridTeam> Teams { get; set; } = new List<ScheduleGridTeam>();

        public int? CurrentPeriodNumber { get; set; }

        public int StartSelectedPeriod { get; set; }
        public int EndSelectedPeriod { get; set; }

        public ScheduleGridColorType ColorType { get; set; } = ScheduleGridColorType.MaxWeeks;
        public ScheduleGridSortBy SortBy { get; set; } = ScheduleGridSortBy.Team;

        // Basketball-only
        public bool UseQualityGames { get; set; } = false;
        public bool ShowEasePositionFilter { get; set; } = false;
        public List<(string Text, string Value)> EasePositionOptions { get; set; } = new List<(string, string)>();
        public string EasePositionFilterValue { get; set; }
    }
}