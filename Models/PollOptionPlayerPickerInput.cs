using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PollOptionPlayerPickerInput
    {
        public string Id { get; set; }
        public List<DisplayPlayerInput> AddedPlayers { get; set; } = new List<DisplayPlayerInput>();
        public List<DisplayPlayerInput> AvailablePlayers { get; set; } = new List<DisplayPlayerInput>();
        public bool CanEdit { get; set; } = true;
    }
}