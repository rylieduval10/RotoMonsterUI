namespace RotoMonsterUI
{
    public class GiveGetPlayerPickerResult
    {
        public bool AddGivePlayerPressed { get; set; }
        public int? SelectedGivePlayerIdToAdd { get; set; }
        public int? RemoveGivePlayerId { get; set; }
        public bool ClearGivePressed { get; set; }

        public bool AddGetPlayerPressed { get; set; }
        public int? SelectedGetPlayerIdToAdd { get; set; }
        public int? RemoveGetPlayerId { get; set; }
        public bool ClearGetPressed { get; set; }

        public bool SwapPressed { get; set; }
    }
}