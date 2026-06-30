using HtmlTags;
using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class WarningIconInput
    {
        public string TeamCode { get; set; }
        public List<WarningPlayer> WarningPlayers { get; set; }
        public PlayerWarningType WarningType { get; set; }
    }

    public class WarningIcon
    {
        private readonly WarningIconInput _input;

        public WarningIcon(WarningIconInput input)
        {
            _input = input;
        }

        public string Render()
        {
            if (_input.WarningPlayers == null) return "";
            var teamWarnings = _input.WarningPlayers.Where(p => p.TeamCode == _input.TeamCode).ToList();
            if (!teamWarnings.Any()) return "";

            var warningColor = _input.WarningType == PlayerWarningType.Alert ? "#FB7185" : "#F59E0B";
            var playerNames = string.Join(", ", teamWarnings.Select(p => $"{p.FirstName} {p.LastName}"));
            var icon = new Icon(new IconInput { Type = IconType.LineupCard, Color = warningColor, Size = 16 }).Render();
            return new CustomTooltip(icon, $"Not in lineup: {playerNames}").Render();
        }
    }
}