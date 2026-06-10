using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class UserCommentCardInput
    {
        public int CommentId { get; set; }
        public DisplayPlayerInput DisplayPlayerInput { get; set; }
        public DisplayUsernameInput DisplayUsernameInput { get; set; }
        public UserVoteInput UserVoteInput { get; set; }
        public string CommentText { get; set; }
        public bool ShowUpDownControls { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public bool UserCanDelete { get; set; }
        public bool UserCanPostComment { get; set; }
        public bool IsCommentExpanded { get; set; }
        public string CurrentCommentText { get; set; }
        public TimeSpan? TimeSinceCreated { get; set; }
        public bool ShowPlayerInfo { get; set; } = true;
        public bool ShowViewAll { get; set; } = false;
        public bool IsNew { get; set; } = false;
    }
}