using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CreatePollResult
    {
        public bool CreatePollPressed { get; set; }
        public bool RefreshRostersPressed { get; set; }

        public string SelectedLeagueId { get; set; }
        public string SelectedLeagueSettingsId { get; set; }
        public string Question { get; set; }
        public int ExpiresInHours { get; set; }
        public Dictionary<int, string> OptionComments { get; set; } = new Dictionary<int, string>();

        public bool AddOptionPressed { get; set; }
        public int? RemoveOptionId { get; set; }
        public int? MoveUpOptionId { get; set; }
        public int? MoveDownOptionId { get; set; }

        public int? AddPlayerOptionId { get; set; }
        public int? SelectedPlayerIdToAdd { get; set; }
        public int? RemovePlayerOptionId { get; set; }
        public int? RemovePlayerId { get; set; }
    }
}