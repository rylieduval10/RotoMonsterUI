namespace RotoMonsterUI
{
    public class FavoritesToolbarResult
    {
        // Set when the user hid a page from the bar.
        public string HidePageId { get; set; }
        // Set when the user picked a page from the "Add a page" dropdown.
        public string AddPageId { get; set; }
    }
}