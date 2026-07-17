using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PageTitleRowInput
    {
        // Pass in the actual rendered PageHeader output (e.g.
        // new PageHeader(title).Render()) so the title stays visually
        // identical to every other page - it renders inline, to the left
        // of the controls, in the same row.
        public string HeaderHtml { get; set; }

        // LEAGUE SELECTOR - hand over the list, the row builds a normal Dropdown.
        public List<(string text, string value)> Leagues { get; set; }
        public string SelectedLeagueValue { get; set; }
        public string LeagueDropdownName { get; set; } = "leagueSelect";
        public string LeagueDropdownLabel { get; set; } = "League";
        // Fallback for pages that build their own. Ignored when Leagues is set.
        public string LeagueDropdownHtml { get; set; }

        // PLAYER SEARCH - autocomplete against a list the site supplies.
        public PlayerSearchInput PlayerSearch { get; set; }
        // Fallback. Ignored when PlayerSearch is set.
        public string SearchBoxHtml { get; set; }

        public bool ShowRefreshRosters { get; set; }
        public string RefreshRostersName { get; set; } = "refreshRosters";

        public bool ShowDarkModeToggle { get; set; } = true;
        public bool IsDarkMode { get; set; }
        // URL that flips light/dark mode (e.g. same page with a toggled
        // ?dark= param) - the site decides how that's wired.
        public string DarkModeToggleUrl { get; set; }

        // Reuses the existing SeasonProgress component (same one used
        // elsewhere in the app) so sizing and the built-in tooltip stay
        // consistent instead of a one-off badge.
        public SeasonProgressInput SeasonProgress { get; set; }

        // Optional - if set, renders as its own bar above the header/controls row.
        public FavoritesToolbarInput FavoritesToolbar { get; set; }
    }
}