using HtmlTags;

namespace RotoMonsterUI
{
    public class VoteControl
    {
        private readonly VoteControlInput _input;

        public VoteControl(VoteControlInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var total = _input.UpVoteCount + _input.DownVoteCount;
            var percent = total > 0 ? (int)System.Math.Round((double)_input.UpVoteCount / total * 100) : 0;
            var percentText = total == 0 ? "no votes" : $"{percent}%";

            var pill = new HtmlTag("span").AddClass("vote-control-pill");
            if (_input.ForceDarkText) pill.Attr("style", "background:rgba(0,0,0,0.1);");

            if (_input.CanVote)
            {
                var upBtn = new HtmlTag("button")
                    .AddClass("vote-control-btn")
                    .Attr("type", "button")
                    .Attr("name", $"{_input.NamePrefix}up_{_input.Id}")
                    .Attr("data-voteid", _input.Id)
                    .Attr("onclick", $"TriggerPostBack(this, '{_input.NamePrefix}up_', 'data-voteid')")
                    .Attr("aria-label", "Upvote");
                if (_input.VotedUp) upBtn.AddClass("vote-control-btn--active-up");
                else if (_input.ForceDarkText) upBtn.Attr("style", "color:#334155;");
                upBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowUp, Size = 14, Color = "currentColor" }).Render());
                pill.Append(upBtn);
            }

            var percentSpan = new HtmlTag("span").AddClass("vote-control-percent").Text(percentText);
            if (_input.ForceDarkText) percentSpan.AddClass("color-shaded");
            pill.Append(percentSpan);

            if (_input.CanVote)
            {
                var downBtn = new HtmlTag("button")
                    .AddClass("vote-control-btn")
                    .Attr("type", "button")
                    .Attr("name", $"{_input.NamePrefix}down_{_input.Id}")
                    .Attr("data-voteid", _input.Id)
                    .Attr("onclick", $"TriggerPostBack(this, '{_input.NamePrefix}down_', 'data-voteid')")
                    .Attr("aria-label", "Downvote");
                if (_input.VotedDown) downBtn.AddClass("vote-control-btn--active-down");
                else if (_input.ForceDarkText) downBtn.Attr("style", "color:#334155;");
                downBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowDown, Size = 14, Color = "currentColor" }).Render());
                pill.Append(downBtn);
            }

            
            if (total > 0)
            {
                var countText = total == 1 ? "1 vote" : $"{total} votes";
                var countSpan = new HtmlTag("span").AddClass("vote-control-count").Text(countText);
                if (_input.ForceDarkText) countSpan.AddClass("color-shaded");
                pill.Append(countSpan);
            }

            if (_input.CanVote)
            {
                var voteStateValue = _input.VotedUp ? "up" : _input.VotedDown ? "down" : "none";
                pill.AppendHtml($"<input type='hidden' name='{_input.NamePrefix}currentvote_{_input.Id}' value='{voteStateValue}' />");
            }

            return pill.ToString();
        }
    }
}