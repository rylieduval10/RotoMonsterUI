using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class UserCommentCardInput
    {
        public DisplayPlayerInput Player { get; set; }
        public DisplayPlayerInput DisplayPlayer { get; set; }
        public string DisplayedUsername { get; set; }
        public string CommentText { get; set; }
        public bool ShowUpDownControls { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public bool UserCanDelete { get; set; }
        public bool UserCanPostComment { get; set; }
        public bool IsCommentExpanded { get; set; }
        public string CurrentCommentText { get; set; }
    }
}