using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CreatePollOptionDraftInput
    {
        public int OptionId { get; set; }
        public string Comment { get; set; }
        public List<DisplayPlayerInput> Players { get; set; } = new List<DisplayPlayerInput>();
    }

    public class SelectableItem
    {
        public string Id { get; set; }
        public string Label { get; set; }
    }

    public class CreatePollInput
    {
        public List<SelectableItem> Leagues { get; set; } = new List<SelectableItem>();
        public string SelectedLeagueId { get; set; }

        public List<SelectableItem> LeagueSettingsOptions { get; set; } = new List<SelectableItem>();
        public string SelectedLeagueSettingsId { get; set; }

        public string Question { get; set; }
        public int ExpiresInHours { get; set; } = 24;
        public int MinExpiresHours { get; set; } = 1;
        public int MaxExpiresHours { get; set; } = 72;

        public int MaxOptions { get; set; } = 6;

        public List<DisplayPlayerInput> AvailablePlayers { get; set; } = new List<DisplayPlayerInput>();
        public List<CreatePollOptionDraftInput> Options { get; set; } = new List<CreatePollOptionDraftInput>
        {
            new CreatePollOptionDraftInput { OptionId = 1 },
            new CreatePollOptionDraftInput { OptionId = 2 }
        };
    }
}