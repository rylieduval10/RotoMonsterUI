using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public enum ChatState
    {
        Watching,
        Chatting
    }

    public class ChatMessageInput
    {
        public int MessageId { get; set; }
        public DisplayUsernameInput DisplayUsernameInput { get; set; }
        public TimeSpan TimeSinceCreated { get; set; }
        public string MessageText { get; set; }

        // Pending confirmation from Ken on exact permission meaning (delete message vs kick user).
        // Modeled for now as per-message delete, matching the CommentCard/NewsCard UserCanDelete pattern.
        public bool UserCanDelete { get; set; } = false;
    }

    public class ChatRoomInput
    {
        public string Id { get; set; }

        public ChatState ChatState { get; set; } = ChatState.Watching;

        public int? UserId { get; set; }

        public List<DisplayUsernameInput> ChattingUsers { get; set; } = new List<DisplayUsernameInput>();

        public int WatchingCount { get; set; }

        public List<ChatMessageInput> Messages { get; set; } = new List<ChatMessageInput>();

        public bool ShowInsertTeamAnalysisOption { get; set; } = true;

        public string MessagePlaceholder { get; set; } = "Enter chat message.";
    }
}