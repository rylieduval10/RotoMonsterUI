using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class FavoritesToolbarResult
    {
        // Set when the user removed the current page from favorites.
        public string HidePageId { get; set; }
        // Set when the user added the current page to favorites.
        public string AddPageId { get; set; }
        // Set when the user drag-reordered the bar - the full new page order.
        // Caller persists this. Null when no reorder happened this postback.
        public List<string> ReorderedPageIds { get; set; }
    }
}