using HtmlTags;

namespace RotoMonsterUI
{
    public class UserVote
    {
        private UserVoteInput _input;

        public UserVote(UserVoteInput input)
        {
            _input = input;
        }

        public string Render()
        {
            if (!_input.HasVoted)
                return "";

            var wrapper = new HtmlTag("span").AddClass("user-vote");

            var voteText = _input.VotedUp ? "You UP" : "You DOWN";
            var voteSpan = new HtmlTag("span")
                .AddClass(_input.VotedUp ? "user-vote-up" : "user-vote-down")
                .Text(voteText);
            wrapper.Append(voteSpan);

            var changeBtn = new HtmlTag("button")
                .AddClass("comment-card-btn user-vote-change")
                .Attr("name", $"changevote_{_input.CommentId}")
                .Text("Change");
            wrapper.Append(changeBtn);

            return wrapper.ToString();
        }
    }
}