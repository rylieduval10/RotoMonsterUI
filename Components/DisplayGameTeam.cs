using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayGameTeam
    {
        private readonly DisplayGameTeamInput _input;

        public DisplayGameTeam(DisplayGameTeamInput input)
        {
            _input = input;
        }

        public string Render()
        {
            bool gameStarted = _input.GameStarted || _input.IsGameLive || _input.IsGameFinished;
            float runs = gameStarted ? _input.CurrentRuns : _input.ProjectedRuns;

            string bgColor = string.IsNullOrEmpty(_input.BgColor)
                ? ColorHelper.GetYellowColorCode(runs, 3.5f, 6.5f, true)
                : _input.BgColor;

            var cell = new HtmlTag("div").AddClass("game-team-cell");
            if (_input.IsWinner && _input.IsGameFinished)
                cell.AddClass("winner");
            cell.Attr("style", $"background-color:#{bgColor};");

            if (!gameStarted)
            {
                cell.AppendHtml(new DisplayLineupDot(new DisplayLineupDotInput { IsConfirmed = _input.LineupConfirmed }).Render());

                if (_input.LineupConfirmed && _input.WarningPlayers != null && _input.WarningType.HasValue)
                {
                    cell.AppendHtml(new DisplayWarningIcon(new DisplayWarningIconInput
                    {
                        TeamCode = _input.TeamCode,
                        WarningPlayers = _input.WarningPlayers,
                        WarningType = _input.WarningType.Value
                    }).Render());
                }
            }

            cell.Append(new HtmlTag("span").AddClass("game-team-code").Text(_input.TeamCode));

            if (gameStarted)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(runs.ToString("0")));
            else if (runs != 0)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(runs.ToString("0.0")));

            if (_input.PlayerCount.HasValue && _input.PlayerCount.Value > 0 && _input.PlayerIconType.HasValue)
            {
                var icon = new Icon(new IconInput { Type = _input.PlayerIconType.Value, Color = _input.PlayerIconColor ?? "#94a3b8", Size = 20 }).Render();
                cell.AppendHtml(new CustomTooltip(icon, $"{_input.PlayerCount} players in this game").Render());
            }

            return cell.ToString();
        }
    }
}