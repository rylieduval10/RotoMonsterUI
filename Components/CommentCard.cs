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

            var ageShadeColor = ColorHelper.GetAgeShadeHex(_input.TimeSinceCreated);
            bool isShaded = ageShadeColor != null;
            if (isShaded)
                card.Attr("style", $"background:{ageShadeColor};");

            // Player title row
            if (_input.ShowPlayerInfo)
            {
                var titleRow = new HtmlTag("div").AddClass("comment-card-title-row d-flex justify-content-between align-items-center");

                // Player name and position badges are left as-is. Team code gets auto-darkened when
                // shaded so it stays readable on yellow without just going flat black.
                var displayPlayerInput = _input.DisplayPlayerInput;
                if (isShaded && !string.IsNullOrEmpty(displayPlayerInput.TeamColor))
                {
                    displayPlayerInput = new DisplayPlayerInput
                    {
                        PlayerName = _input.DisplayPlayerInput.PlayerName,
                        PlayerId = _input.DisplayPlayerInput.PlayerId,
                        TeamCode = _input.DisplayPlayerInput.TeamCode,
                        TeamColor = ColorHelper.GetAutoColorForLightBackground(_input.DisplayPlayerInput.TeamColor),
                        Positions = _input.DisplayPlayerInput.Positions
                    };
                }
                var playerDisplay = new DisplayPlayer(displayPlayerInput).Render();
                var playerTitle = new HtmlTag("span").AddClass("comment-card-player d-flex align-items-center gap-2").AppendHtml(playerDisplay);

                if (_input.ShowViewAll)
                {
                    var viewAll = new HtmlTag("a")
                        .AddClass("comment-card-viewall")
                        .Attr("href", $"/usercomments.aspx?i={_input.DisplayPlayerInput.PlayerId}")
                        .Text("view all");
                    if (isShaded) viewAll.AddClass("color-shaded");
                    playerTitle.Append(viewAll);
                }

                titleRow.Append(playerTitle);

                if (_input.TimeSinceCreated.HasValue)
                {
                    var timeSince = new HtmlTag("span").AppendHtml(new TimeSince(_input.TimeSinceCreated.Value).Render());
                    if (isShaded) timeSince.AddClass("color-shaded");
                    titleRow.Append(timeSince);
                }

                card.Append(titleRow);
            }
            else if (_input.TimeSinceCreated.HasValue)
            {
                var timeRow = new HtmlTag("div").AddClass("comment-card-title-row d-flex justify-content-end");
                var timeSince = new HtmlTag("span").AppendHtml(new TimeSince(_input.TimeSinceCreated.Value).Render());
                if (isShaded) timeSince.AddClass("color-shaded");
                timeRow.Append(timeSince);
                card.Append(timeRow);
            }

            // Username row with optional New badge
            // Username is NOT force-shaded - Ken has something specific planned for that separately.
            var usernameRow = new HtmlTag("div").AddClass("comment-card-username d-flex align-items-center gap-2");
            usernameRow.AppendHtml(new DisplayUsername(_input.DisplayUsernameInput).Render());

            if (_input.IsNew)
            {
                // Badge keeps its own background/text color regardless of shading - it already has enough contrast on its own.
                var newBadge = new Badge(new BadgeInput
                {
                    BadgeText = "New",
                    ColorClass = "badge-new"
                }).Render();
                usernameRow.AppendHtml(newBadge);
            }

            card.Append(usernameRow);

            // Comment text
            var commentText = new HtmlTag("div").AddClass("comment-card-text").Text(_input.CommentText);
            if (isShaded) commentText.AddClass("color-shaded");
            card.Append(commentText);

            // Actions row - vote buttons and delete icon keep their own styling (borders/explicit colors already read fine on yellow)
            var actionsRow = new HtmlTag("div").AddClass("comment-card-actions");

            if (_input.ShowUpDownControls)
            {
                if (_input.CanVote && (_input.UserVoteInput == null || !_input.UserVoteInput.HasVoted))
                {
                    var upBtn = new HtmlTag("button")
                        .AddClass("comment-card-btn comment-card-btn-up")
                        .Attr("name", $"upvote_{_input.CommentId}")
                        .Text("Up");
                    var downBtn = new HtmlTag("button")
                        .AddClass("comment-card-btn comment-card-btn-down")
                        .Attr("name", $"downvote_{_input.CommentId}")
                        .Text("Down");
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
                var trashIcon = new Icon(new IconInput { Type = IconType.Trash, Size = 16, Color = "#ef4444" }).Render();
                var deleteBtn = new HtmlTag("button")
                    .AddClass("comment-card-btn comment-card-btn-delete")
                    .Attr("name", $"delete_{_input.CommentId}")
                    .Attr("style", "margin-left: auto;")
                    .AppendHtml(trashIcon);
                actionsRow.Append(deleteBtn);
            }

            if (_input.UserCanPostComment && !_input.IsCommentExpanded)
            {
                var expandBtn = new HtmlTag("button")
                    .AddClass("comment-card-btn comment-card-btn-expand")
                    .Attr("name", $"expand_{_input.CommentId}");
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
                    .Attr("name", $"comment_{_input.CommentId}")
                    .Text(_input.CurrentCommentText ?? "");
                var postBtn = new HtmlTag("button")
                    .AddClass("comment-card-btn comment-card-btn-post")
                    .Attr("name", $"post_{_input.CommentId}")
                    .Text("Post");
                expandedArea.Append(textarea);
                expandedArea.Append(postBtn);
                card.Append(expandedArea);
            }

            return card.ToString();
        }
    }
}