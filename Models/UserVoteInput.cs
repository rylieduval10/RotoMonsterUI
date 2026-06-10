namespace RotoMonsterUI
{
    public class UserVoteInput
    {
        public bool HasVoted { get; set; } = false;
        public bool VotedUp { get; set; } = false;
        public bool VotedDown { get; set; } = false;
        public int CommentId { get; set; }
    }
}