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
        // Pages the user has favorited, in display order.
        public List<FavoritePageItem> Pages { get; set; } = new List<FavoritePageItem>();
        public FavoritePageItem CurrentPage { get; set; }
        public int MaxPages { get; set; } = 5;
    }
}