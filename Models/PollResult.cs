namespace RotoMonsterUI
{
    public class PollResult
    {
        public int? ChosenOptionId { get; set; }
        public bool ClearVotePressed { get; set; }
        public bool DeletePollPressed { get; set; }
        public int? ToggleEditPlayersOptionId { get; set; }
        public int? AddPlayerOptionId { get; set; }
        public int? SelectedPlayerIdToAdd { get; set; }
        public int? RemovePlayerOptionId { get; set; }
        public int? RemovePlayerId { get; set; }
    }
}