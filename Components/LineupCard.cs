using HtmlTags;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class LineupCard
    {
        private readonly LineupCardInput _input;

        public LineupCard(LineupCardInput input)
        {
            _input = input;
        }

        private HtmlTag BuildTeamHeader(LineupCardTeamInput team)
        {
            var header = new HtmlTag("div").AddClass("lineup-card-team-header");

            var nameRow = new HtmlTag("div").AddClass("lineup-card-name-row");

            var teamCode = new HtmlTag("span").AddClass("lineup-card-team-code");
            var teamColor = !string.IsNullOrEmpty(team.TeamColor) 
                ? team.TeamColor 
                : TeamColorHelper.GetTeamColor(team.TeamCode, _input.IsDarkMode);
            if (teamColor != null)
                teamCode.Attr("style", $"color:{teamColor};");
            var teamText = team.TeamCode;
            if (team.Rank.HasValue)
                teamText += $" ({team.Rank})";
            if (team.IsHomeTeam)
                teamText = "@ " + teamText;
            teamCode.Text(teamText);

            nameRow.Append(teamCode);
            header.Append(nameRow);

            if (!string.IsNullOrEmpty(team.OddsLine))
            {
                var odds = new HtmlTag("div").AddClass("lineup-card-odds").Text(team.OddsLine);
                header.Append(odds);
            }

            return header;
        }

        private HtmlTag BuildPlayerTable(LineupCardTeamInput team)
        {
            var wrapper = new HtmlTag("div").AddClass("lineup-card-table-wrapper");

            // Column header
            var colHeader = new HtmlTag("div").AddClass("lineup-card-col-header");
            colHeader.AppendHtml("<span class='lineup-card-col-num'>#</span>");
            colHeader.AppendHtml($"<span class='lineup-card-col-name'>Player vs. {team.PlayerVsHandedness}</span>");
            wrapper.Append(colHeader);

            // Player rows
            foreach (var player in team.Players)
            {
                var row = new HtmlTag("div").AddClass("lineup-card-player-row");

                var num = new HtmlTag("span").AddClass("lineup-card-player-num");
                num.Text(player.IsStartingPitcher ? "" : player.BattingOrder?.ToString() ?? "");

                var name = new HtmlTag("span").AddClass("lineup-card-player-name");
                    name.AppendHtml($"{player.Name} ");

                var hand = new HtmlTag("span").AddClass("lineup-card-player-hand").Text(player.Handedness);
                    name.Append(hand);

                var pos = new HtmlTag("span").AddClass("lineup-card-player-pos");
                if (!string.IsNullOrEmpty(player.PositionColor))
                    pos.Attr("style", $"color:{player.PositionColor};");
                pos.Text(player.Position);

                row.Append(num);
                row.Append(name);
                row.Append(pos);
                wrapper.Append(row);
            }

            // Verified / Expected footer
            var footer = new HtmlTag("div").AddClass("lineup-card-footer");
            if (team.IsVerified)
            {
                footer.AddClass("lineup-card-footer--verified");
                footer.AppendHtml("&#10003; VERIFIED LINEUP");
            }
            else if (team.LineupExpectedMinutes.HasValue)
            {
                footer.AddClass("lineup-card-footer--expected");
                footer.Text($"Lineup Expected {team.LineupExpectedMinutes}m");
            }

            if (team.IsVerified || team.LineupExpectedMinutes.HasValue)
                wrapper.Append(footer);

            return wrapper;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("lineup-card");

            if (!string.IsNullOrEmpty(_input.Id))
                card.Attr("id", _input.Id);

            // Game row at top using RenderSingleGameInner
            if (_input.Game != null)
            {
                var gameRow = new HtmlTag("div").AddClass("lineup-card-game-row game-row-inner");
                gameRow.AppendHtml(new DisplayGames(_input.Game).RenderSingleGameInner());
                card.Append(gameRow);
            }

            // Two column layout
            var columns = new HtmlTag("div").AddClass("lineup-card-columns");

            // Away team
            var awayCol = new HtmlTag("div").AddClass("lineup-card-col");
            awayCol.Append(BuildTeamHeader(_input.AwayTeam));
            awayCol.Append(BuildPlayerTable(_input.AwayTeam));
            columns.Append(awayCol);

            // Home team
            var homeCol = new HtmlTag("div").AddClass("lineup-card-col");
            homeCol.Append(BuildTeamHeader(_input.HomeTeam));
            homeCol.Append(BuildPlayerTable(_input.HomeTeam));
            columns.Append(homeCol);

            card.Append(columns);

            return card.ToString();
        }
    }
}