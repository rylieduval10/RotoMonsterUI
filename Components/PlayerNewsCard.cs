using HtmlTags;

namespace RotoMonsterUI
{
    public enum PlayerStatus
    {
        Starting,
        Doubtful,
        Out,
        Questionable
    }

    public class PlayerNewsCard
    {
        private string _playerName;
        private string _teamInfo;
        private string _note;
        private PlayerStatus? _status;

        public PlayerNewsCard WithPlayerName(string name)
        {
            _playerName = name;
            return this;
        }

        public PlayerNewsCard WithTeamInfo(string teamInfo)
        {
            _teamInfo = teamInfo;
            return this;
        }

        public PlayerNewsCard WithNote(string note)
        {
            _note = note;
            return this;
        }

        public PlayerNewsCard WithStatus(PlayerStatus status)
        {
            _status = status;
            return this;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("player-news-card");

            if (_status.HasValue)
                card.AddClass($"player-news-card--{_status.Value.ToString().ToLower()}");

            if (_status.HasValue)
            {
                var statusBadge = new HtmlTag("div")
                    .AddClass("player-news-status")
                    .AddClass($"player-news-status--{_status.Value.ToString().ToLower()}")
                    .Text(_status.Value.ToString().ToUpper());
                card.Append(statusBadge);
            }

            if (!string.IsNullOrEmpty(_playerName))
            {
                var name = new HtmlTag("div")
                    .AddClass("player-news-name")
                    .Text(_playerName);
                card.Append(name);
            }

            if (!string.IsNullOrEmpty(_teamInfo))
            {
                var teamInfo = new HtmlTag("div")
                    .AddClass("player-news-team")
                    .Text(_teamInfo);
                card.Append(teamInfo);
            }

            if (!string.IsNullOrEmpty(_note))
            {
                var note = new HtmlTag("div")
                    .AddClass("player-news-note")
                    .Text(_note);
                card.Append(note);
            }

            return card.ToString();
        }
    }
}