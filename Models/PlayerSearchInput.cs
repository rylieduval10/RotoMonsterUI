using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PlayerSearchInput
    {
        public string Id { get; set; } = "playerSearch";

        // Ken passes the searchable list. Same shape the poll picker takes.
        public List<DisplayPlayerInput> AvailablePlayers { get; set; } = new List<DisplayPlayerInput>();

        public string Placeholder { get; set; } = "Search players...";

        public string UrlFormat { get; set; }

        public int MaxResults { get; set; } = 8;
    }
}