using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PageTitleRowService
    {
        private readonly PlayerSearchService _playerSearchService = new PlayerSearchService();
        private readonly FavoritesToolbarService _favoritesToolbarService = new FavoritesToolbarService();

        public PageTitleRowResult Process(PageTitleRowInput input, Dictionary<string, string> formValues)
        {
            var result = new PageTitleRowResult();

            if (input == null || formValues == null)
                return result;

            // League selection
            if (!string.IsNullOrEmpty(input.LeagueDropdownName)
                && formValues.TryGetValue(input.LeagueDropdownName, out var leagueValue)
                && !string.IsNullOrEmpty(leagueValue))
            {
                result.SelectedLeagueValue = leagueValue;
            }

            // Refresh rosters 
            if (input.ShowRefreshRosters && !string.IsNullOrEmpty(input.RefreshRostersName))
            {
                formValues.TryGetValue("__EVENTTARGET", out var eventTarget);
                if (formValues.ContainsKey(input.RefreshRostersName)
                    || eventTarget == input.RefreshRostersName)
                {
                    result.RefreshRostersClicked = true;
                }
            }

            // Player search
            if (input.PlayerSearch != null)
            {
                var playerResult = _playerSearchService.Process(input.PlayerSearch.Id, formValues);
                result.SelectedPlayerId = playerResult.SelectedPlayerId;
            }

            // Favorites toolbar
            if (input.FavoritesToolbar != null)
            {
                var favResult = _favoritesToolbarService.Process(input.FavoritesToolbar.Id, formValues);
                result.AddFavoritePageId = favResult.AddPageId;
                result.HideFavoritePageId = favResult.HidePageId;
            }

            return result;
        }
    }
}