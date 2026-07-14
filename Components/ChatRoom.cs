using System.Linq;
using HtmlTags;

namespace RotoMonsterUI
{
    public class ChatRoom
    {
        private readonly ChatRoomInput _input;

        public ChatRoom(ChatRoomInput input)
        {
            _input = input;
        }

        public string Render()
        {
            var container = new HtmlTag("div").AddClass("chat-room");

            if (_input.ChatState == ChatState.Watching)
            {
                var joinBtn = new Button("Join Chat")
                    .WithStyle(ButtonStyle.Secondary)
                    .WithName($"joinchat_{_input.Id}")
                    .Render();
                container.AppendHtml(joinBtn);
            }
            else
            {
                var switchBtn = new Button("Switch to Watching")
                    .WithStyle(ButtonStyle.Secondary)
                    .WithName($"switchwatching_{_input.Id}")
                    .Render();
                container.AppendHtml(switchBtn);

                var editor = new RichTextEditor(new RichTextEditorInput
                {
                    Id = $"{_input.Id}-editor",
                    Name = $"{_input.Id}-editor-value",
                    Placeholder = _input.MessagePlaceholder,
                    MinHeight = 100
                }).Render();
                container.AppendHtml(editor);

                var actionRow = new HtmlTag("div").AddClass("chat-room-actions d-flex align-items-center gap-2");

                var postBtn = new Button("Post Message")
                    .WithStyle(ButtonStyle.Success)
                    .WithName($"postmessage_{_input.Id}")
                    .Render();
                actionRow.AppendHtml(postBtn);

                if (_input.ShowInsertTeamAnalysisOption)
                {
                    var checkbox = new Checkbox()
                        .WithLabel("Insert Team Analysis at end of post")
                        .WithName($"inserttA_{_input.Id}")
                        .WithPostBack()
                        .Render();
                    actionRow.AppendHtml(checkbox);
                }

                container.Append(actionRow);
            }

            // Chatting Users / Watching line
            var statusRow = new HtmlTag("div").AddClass("chat-room-status");
            var chattingLabel = new HtmlTag("span")
                .Text($"Chatting Users ({_input.ChattingUsers.Count}): ");
            statusRow.Append(chattingLabel);

            for (int i = 0; i < _input.ChattingUsers.Count; i++)
            {
                var user = _input.ChattingUsers[i];
                statusRow.AppendHtml(new DisplayUsername(user).Render());
                if (i < _input.ChattingUsers.Count - 1)
                    statusRow.AppendHtml(", ");
            }

            statusRow.AppendHtml($"&nbsp;&nbsp;Watching ({_input.WatchingCount})");
            container.Append(statusRow);

            // Message list
            var list = new HtmlTag("div").AddClass("chat-room-messages");
            foreach (var message in _input.Messages)
                list.Append(RenderMessage(message));

            container.Append(list);

            return container.ToString();
        }

        private HtmlTag RenderMessage(ChatMessageInput message)
        {
            var row = new HtmlTag("div").AddClass("chat-card d-flex align-items-center gap-2");

            row.AppendHtml(new TimeSince(message.TimeSinceCreated).Render());

            var bubble = new HtmlTag("span").AddClass("chat-card-bubble");
            bubble.AppendHtml(new Icon(new IconInput { Type = IconType.ChatBubble, Size = 16, Color = "white" }).Render());
            row.Append(bubble);

            var usernameInput = message.DisplayUsernameInput;
            row.AppendHtml(new DisplayUsername(usernameInput).Render());

            if (message.UserCanDelete)
            {
                var deleteBtn = new HtmlTag("button")
                    .AddClass("chat-card-delete")
                    .Attr("type", "button")
                    .Attr("name", $"deletemsg_{_input.Id}_{message.MessageId}")
                    .Attr("aria-label", "Delete message")
                    .AppendHtml(new Icon(new IconInput { Type = IconType.Close, Size = 14, Color = "#ef4444" }).Render());
                row.Append(deleteBtn);
            }

            var text = new HtmlTag("span").AddClass("chat-card-text").Text(message.MessageText);
            row.Append(text);

            return row;
        }
    }
}