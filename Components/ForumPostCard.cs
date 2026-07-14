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
                if (_input.UserVoteInput == null || !_input.UserVoteInput.HasVoted)
                {
                    var upBtn = new HtmlTag("button")
                        .AddClass("forum-post-card-btn forum-post-card-btn-up")
                        .Attr("name", $"forumupvote_{_input.PostId}")
                        .Text("Up");
                    var downBtn = new HtmlTag("button")
                        .AddClass("forum-post-card-btn forum-post-card-btn-down")
                        .Attr("name", $"forumdownvote_{_input.PostId}")
                        .Text("Down");
                    actionsRow.Append(upBtn);
                    actionsRow.Append(downBtn);
                }

                var total = _input.UpVoteCount + _input.DownVoteCount;
                var percent = total > 0 ? (int)Math.Round((double)_input.UpVoteCount / total * 100) : 0;
                var voteCount = new HtmlTag("span").AddClass("forum-post-card-votes")
                    .Text($"{percent}% ({total}) UP");
                actionsRow.Append(voteCount);

                if (_input.UserVoteInput != null && _input.UserVoteInput.HasVoted)
                {
                    // Reuses UserVote, just needs the vote input's CommentId to line up with PostId for the change-vote button name.
                    var userVote = new HtmlTag("span").AppendHtml(new UserVote(_input.UserVoteInput).Render());
                    actionsRow.Append(userVote);
                }
            }

            if (_input.UserCanEdit)
            {
                var editBtn = new HtmlTag("button")
                    .AddClass("forum-post-card-btn forum-post-card-btn-edit")
                    .Attr("type", "button")
                    .Attr("name", $"forumedit_{_input.PostId}")
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
                    .Attr("name", $"forumdelete_{_input.PostId}")
                    .Attr("style", "margin-left: auto;")
                    .AppendHtml(trashIcon);
                actionsRow.Append(deleteBtn);
            }

            card.Append(actionsRow);

            return card.ToString();
        }
    }
}