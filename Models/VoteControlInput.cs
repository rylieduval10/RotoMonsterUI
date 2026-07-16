namespace RotoMonsterUI
{
    public class VoteControlInput
    {
        public string Id { get; set; }
        public string NamePrefix { get; set; } = "vote";
        public bool CanVote { get; set; } = true;
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public bool VotedUp { get; set; }
        public bool VotedDown { get; set; }

        // Set true when the control renders on a bright background (like CommentCard's
        // yellow "fresh" shading) so text/icons stay readable instead of nearly invisible.
        public bool ForceDarkText { get; set; } = false;
    }
}