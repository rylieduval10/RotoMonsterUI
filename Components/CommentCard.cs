using System;
using HtmlTags;

namespace RotoMonsterUI
{
    public class CommentCard
    {
        private UserCommentCardInput _input;

        public CommentCard(UserCommentCardInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var card = new HtmlTag("div").AddClass("comment-card");

            // Player title row
            var titleRow = new HtmlTag("div").AddClass("comment-card-title-row");
            var playerDisplay = new DisplayPlayer(_input.DisplayPlayerInput).Render();
            var playerTitle = new HtmlTag("span").AddClass("comment-card-player").AppendHtml(playerDisplay);
            var viewAll = new HtmlTag("a")
                .AddClass("comment-card-viewall")
                .Attr("href", $"/usercomments.aspx?i={_input.DisplayPlayerInput.PlayerId}")
                .Text("view all");
            titleRow.Append(playerTitle);
            titleRow.Append(viewAll);
            card.Append(titleRow);

            // Username
            var username = new HtmlTag("div").AddClass("comment-card-username")
                .AppendHtml(new DisplayUsername(_input.DisplayUsernameInput).Render());
            card.Append(username);

            // Comment text
            var commentText = new HtmlTag("div").AddClass("comment-card-text").Text(_input.CommentText);
            card.Append(commentText);

            // Actions row
            var actionsRow = new HtmlTag("div").AddClass("comment-card-actions");

            if (_input.ShowUpDownControls)
            {
                if (_input.UserVoteInput == null || !_input.UserVoteInput.HasVoted)
                {
                    var upBtn = new HtmlTag("button").AddClass("comment-card-btn comment-card-btn-up").Text("Up");
                    var downBtn = new HtmlTag("button").AddClass("comment-card-btn comment-card-btn-down").Text("Down");
                    actionsRow.Append(upBtn);
                    actionsRow.Append(downBtn);
                }

                var total = _input.UpVoteCount + _input.DownVoteCount;
                var percent = total > 0 ? (int)Math.Round((double)_input.UpVoteCount / total * 100) : 0;
                var voteCount = new HtmlTag("span").AddClass("comment-card-votes")
                    .Text($"{percent}% ({total}) UP");
                actionsRow.Append(voteCount);

                if (_input.UserVoteInput != null)
                {
                    var userVote = new HtmlTag("span").AppendHtml(new UserVote(_input.UserVoteInput).Render());
                    actionsRow.Append(userVote);
                }
            }

            if (_input.UserCanDelete)
            {
                var deleteBtn = new HtmlTag("button").AddClass("comment-card-btn comment-card-btn-delete").Text("delete");
                actionsRow.Append(deleteBtn);
            }

            if (_input.UserCanPostComment && !_input.IsCommentExpanded)
            {
                var expandBtn = new HtmlTag("button").AddClass("comment-card-btn comment-card-btn-expand");
                expandBtn.AppendHtml("<i class='fas fa-reply'></i>");
                actionsRow.Append(expandBtn);
            }

            card.Append(actionsRow);

            // Comment input area (if expanded)
            if (_input.IsCommentExpanded)
            {
                var expandedArea = new HtmlTag("div").AddClass("comment-card-expanded");
                var textarea = new HtmlTag("textarea")
                    .AddClass("comment-card-textarea")
                    .Text(_input.CurrentCommentText ?? "");
                var postBtn = new HtmlTag("button").AddClass("comment-card-btn comment-card-btn-post").Text("Post");
                expandedArea.Append(textarea);
                expandedArea.Append(postBtn);
                card.Append(expandedArea);
            }

            return card.ToString();
        }
    }
}