using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class FavoritePageItem
    {
        public string PageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class FavoritesToolbarInput
    {
        public string Id { get; set; }
        // Pages the user has manually added, in display order.
        public List<FavoritePageItem> Pages { get; set; } = new List<FavoritePageItem>();
        // Pages not currently favorited that the user can pick from the
        // "Add a page" dropdown.
        public List<FavoritePageItem> AvailablePages { get; set; } = new List<FavoritePageItem>();
        public int MaxPages { get; set; } = 5;
    }
}