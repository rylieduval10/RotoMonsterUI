using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class LeagueOption
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class PageTitleRowInput
    {

        public string HeaderHtml { get; set; }

        public List<LeagueOption> Leagues { get; set; }
        public string SelectedLeagueValue { get; set; }
        public string LeagueDropdownLabel { get; set; } = "League";

        public string LeagueDropdownHtml { get; set; }
        public PlayerSearchInput PlayerSearch { get; set; }
        public string SearchBoxHtml { get; set; }

        public bool ShowRefreshRosters { get; set; }

        public bool ShowDarkModeToggle { get; set; } = true;
        public bool IsDarkMode { get; set; }

        public SeasonProgressInput SeasonProgress { get; set; }
        public FavoritesToolbarInput FavoritesToolbar { get; set; }
    }
}