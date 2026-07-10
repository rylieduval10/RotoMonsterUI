using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class TrackedLeaguesTable
    {
        private readonly List<TrackedLeagueBarInput> _leagues;
        private readonly string _title;

        public TrackedLeaguesTable(List<TrackedLeagueBarInput> leagues, string title = "Tracked Leagues")
        {
            _leagues = leagues;
            _title = title;
        }

        public string Render()
        {
            var rows = new TrackedLeagueBar(_leagues).Render();

            return new FlatContainer()
                .WithTitle(_title)
                .WithContent(rows)
                .Render();
        }
    }
}