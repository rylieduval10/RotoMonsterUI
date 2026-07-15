using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class ForumPostCard
    {
        private ForumPostCardInput _input;

        public ForumPostCard(ForumPostCardInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("forum-post-card");

            // Username row (with avatar + post count via DisplayUsername) + New badge + TimeSince
            var usernameRow = new HtmlTag("div").AddClass("forum-post-card-username d-flex align-items-center gap-2");
            _input.DisplayUsernameInput.ShowAvatar = true;
            usernameRow.AppendHtml(new DisplayUsername(_input.DisplayUsernameInput).Render());

            if (_input.IsNew)
            {
                var newBadge = new Badge(new BadgeInput
                {
                    BadgeText = "New",
                    ColorClass = "badge-new"
                }).Render();
                usernameRow.AppendHtml(newBadge);
            }

            if (_input.TimeSinceCreated.HasValue)
            {
                var timeSince = new HtmlTag("span")
                    .AddClass("forum-post-card-time")
                    .AppendHtml(new TimeSince(_input.TimeSinceCreated.Value).Render());
                usernameRow.Append(timeSince);
            }

            card.Append(usernameRow);

            // Post text - rendered as HTML since posts can contain formatting (lists, links) from the rich text editor
            var postText = new HtmlTag("div").AddClass("forum-post-card-text").AppendHtml(_input.PostText);
            card.Append(postText);

            // Actions row - vote controls, edit, delete
            var actionsRow = new HtmlTag("div").AddClass("forum-post-card-actions");

            if (_input.ShowUpDownControls)
            {
                bool votedUp = _input.UserVoteInput != null && _input.UserVoteInput.HasVoted && _input.UserVoteInput.VotedUp;
                bool votedDown = _input.UserVoteInput != null && _input.UserVoteInput.HasVoted && _input.UserVoteInput.VotedDown;

                var total = _input.UpVoteCount + _input.DownVoteCount;
                var percent = total > 0 ? (int)Math.Round((double)_input.UpVoteCount / total * 100) : 0;

                var votePill = new HtmlTag("span").AddClass("forum-post-vote-pill");

                if (_input.CanVote)
                {
                    var upBtn = new HtmlTag("button")
                        .AddClass("forum-post-vote-btn")
                        .Attr("type", "button")
                        .Attr("name", $"forumupvote_{_input.PostId}")
                        .Attr("data-postid", _input.PostId.ToString())
                        .Attr("onclick", "TriggerPostBack(this, 'forumupvote_', 'data-postid')")
                        .Attr("aria-label", "Upvote");
                    if (votedUp) upBtn.AddClass("forum-post-vote-btn--active-up");
                    upBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowUp, Size = 14, Color = "currentColor" }).Render());
                    votePill.Append(upBtn);
                }

                var percentSpan = new HtmlTag("span").AddClass("forum-post-vote-percent").Text($"{percent}%");
                votePill.Append(percentSpan);

                if (_input.CanVote)
                {
                    var downBtn = new HtmlTag("button")
                        .AddClass("forum-post-vote-btn")
                        .Attr("type", "button")
                        .Attr("name", $"forumdownvote_{_input.PostId}")
                        .Attr("data-postid", _input.PostId.ToString())
                        .Attr("onclick", "TriggerPostBack(this, 'forumdownvote_', 'data-postid')")
                        .Attr("aria-label", "Downvote");
                    if (votedDown) downBtn.AddClass("forum-post-vote-btn--active-down");
                    downBtn.AppendHtml(new Icon(new IconInput { Type = IconType.ArrowDown, Size = 14, Color = "currentColor" }).Render());
                    votePill.Append(downBtn);
                }

                actionsRow.Append(votePill);

                var voteCount = new HtmlTag("span").AddClass("forum-post-vote-count")
                    .Text(total == 1 ? "1 vote" : $"{total} votes");
                actionsRow.Append(voteCount);

                if (_input.CanVote)
                {
                    // Records the current vote state at render time so the service can tell "vote again"
                    // apart from "clicking the same arrow means remove my vote".
                    var voteStateValue = votedUp ? "up" : votedDown ? "down" : "none";
                    actionsRow.AppendHtml($"<input type='hidden' name='forumcurrentvote_{_input.PostId}' value='{voteStateValue}' />");
                }
            }

            if (_input.UserCanEdit)
            {
                var editBtn = new HtmlTag("button")
                    .AddClass("forum-post-card-btn forum-post-card-btn-edit")
                    .Attr("type", "button")
                    .Attr("name", $"forumedit_{_input.PostId}")
                    .Attr("data-postid", _input.PostId.ToString())
                    .Attr("onclick", "TriggerPostBack(this, 'forumedit_', 'data-postid')")
                    .Attr("aria-label", "Edit post");
                editBtn.AppendHtml(new Icon(new IconInput { Type = IconType.Edit, Size = 15 }).Render());
                editBtn.AppendHtml("Edit");
                actionsRow.Append(editBtn);
            }

            if (_input.UserCanDelete)
            {
                var trashIcon = new Icon(new IconInput { Type = IconType.Trash, Size = 16, Color = "#ef4444" }).Render();
                var deleteBtn = new HtmlTag("button")
                    .AddClass("forum-post-card-btn forum-post-card-btn-delete")
                    .Attr("type", "button")
                    .Attr("name", $"forumdelete_{_input.PostId}")
                    .Attr("data-postid", _input.PostId.ToString())
                    .Attr("onclick", "TriggerPostBack(this, 'forumdelete_', 'data-postid')")
                    .Attr("style", "margin-left: auto;")
                    .AppendHtml(trashIcon);
                actionsRow.Append(deleteBtn);
            }

            card.Append(actionsRow);

            return card.ToString();
        }
    }
}