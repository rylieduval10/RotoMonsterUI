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

            // Vote count now lives inside the pill - person icon, click for a "N vote(s)" tooltip
            var tooltipId = $"votecontrol-tooltip-{_input.NamePrefix}-{_input.Id}";
            var countWrap = new HtmlTag("span").AddClass("bm-tooltip-wrap vote-control-count-wrap");
            var countTrigger = new HtmlTag("span")
                .AddClass("vote-control-count bm-tooltip-trigger")
                .Attr("data-bm-tooltip", tooltipId);
            if (_input.ForceDarkText) countTrigger.AddClass("color-shaded");
            countTrigger.AppendHtml($"<span class='vote-control-count-number'>{total}</span>");
            countTrigger.AppendHtml(new Icon(new IconInput { Type = IconType.PersonSimple, Size = 14, Color = "currentColor" }).Render());
            countWrap.Append(countTrigger);

            var tooltipText = total == 0 ? "no votes" : (total == 1 ? "1 vote" : $"{total} votes");
            countWrap.Append(new HtmlTag("div").AddClass("bm-tooltip-content bm-tooltip-content--centered").Attr("id", tooltipId).Text(tooltipText));
            pill.Append(countWrap);

            if (_input.CanVote)
            {
                var voteStateValue = _input.VotedUp ? "up" : _input.VotedDown ? "down" : "none";
                pill.AppendHtml($"<input type='hidden' name='{_input.NamePrefix}currentvote_{_input.Id}' value='{voteStateValue}' />");
            }

            return pill.ToString();
        }
    }
}