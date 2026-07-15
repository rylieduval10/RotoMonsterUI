using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class GiveGetPlayerPickerInput
    {
        public string Id { get; set; }
        public bool CanEdit { get; set; } = true;
        public List<DisplayPlayerInput> GivePlayers { get; set; } = new List<DisplayPlayerInput>();
        public List<DisplayPlayerInput> GetPlayers { get; set; } = new List<DisplayPlayerInput>();
        public string GiveSearchPlaceholder { get; set; } = "Search your roster...";
        public string GetSearchPlaceholder { get; set; } = "Search all players...";
    }
}