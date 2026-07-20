namespace RotoMonsterUI
{
    public class FavoritesToolbarResult
    {
        // Set when the user removed the current page from favorites.
        public string HidePageId { get; set; }
        // Set when the user added the current page to favorites.
        public string AddPageId { get; set; }
    }
}