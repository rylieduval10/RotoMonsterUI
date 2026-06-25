using HtmlTags;

namespace RotoMonsterUI
{
    public class DisplayTeamCodeInput
    {
        public string TeamCode { get; set; }
        public float Runs { get; set; }
        public string BgColor { get; set; }
        public bool IsWinner { get; set; }
        public bool IsGameFinished { get; set; }
        public bool GameStarted { get; set; }
    }

    public class DisplayTeamCode
    {
        private readonly DisplayTeamCodeInput _input;

        public DisplayTeamCode(DisplayTeamCodeInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var cell = new HtmlTag("div").AddClass("game-team-cell");
            if (_input.IsWinner && _input.IsGameFinished)
                cell.AddClass("winner");
            cell.Attr("style", $"background-color:#{_input.BgColor};");

            cell.Append(new HtmlTag("span").AddClass("game-team-code").Text(_input.TeamCode));

            if (_input.GameStarted)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(_input.Runs.ToString("0")));
            else if (_input.Runs != 0)
                cell.Append(new HtmlTag("span").AddClass("game-team-runs").Text(_input.Runs.ToString("0.0")));

            return cell.ToString();
        }
    }
}