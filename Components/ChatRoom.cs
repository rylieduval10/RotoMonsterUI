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

        public string RenderControls()
        {
            var container = new HtmlTag("div").AddClass("chat-room-controls");

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

                var actionRow = new HtmlTag("div").AddClass("chat-room-actions");

                var postBtn = new Button("Post Message")
                    .WithStyle(ButtonStyle.Primary)
                    .WithName($"postmessage_{_input.Id}")
                    .Render();
                actionRow.AppendHtml(postBtn);

                if (_input.ShowInsertTeamAnalysisOption)
                {
                    var checkbox = new Checkbox()
                        .WithLabel("Insert Team Analysis at end of post")
                        .WithName($"inserttA_{_input.Id}")
                        .Render();
                    actionRow.AppendHtml(checkbox);
                }

                container.Append(actionRow);
            }

            return container.ToString();
        }

        public string RenderChatLog()
        {
            var container = new HtmlTag("div").AddClass("chat-room-log");

            // Chatting Users / Watching line - pill badges with a small "active" dot
            var statusRow = new HtmlTag("div").AddClass("chat-room-status");

            foreach (var user in _input.ChattingUsers)
            {
                var pill = new HtmlTag("span").AddClass("chat-room-user-pill");
                pill.AppendHtml("<span class=\"chat-room-user-dot\"></span>");
                pill.AppendHtml(new DisplayUsername(user).Render());
                statusRow.Append(pill);
            }

            if (_input.WatchingCount > 0)
            {
                var watchingLabel = new HtmlTag("span")
                    .AddClass("chat-room-watching-label")
                    .Text($"+{_input.WatchingCount} watching");
                statusRow.Append(watchingLabel);
            }

            container.Append(statusRow);

            // Message list
            var list = new HtmlTag("div").AddClass("chat-room-messages");
            foreach (var message in _input.Messages)
                list.Append(RenderMessage(message));

            container.Append(list);

            return container.ToString();
        }

        public string Render()
        {
            var container = new HtmlTag("div").AddClass("chat-room");
            container.AppendHtml(RenderControls());
            container.AppendHtml(RenderChatLog());
            return container.ToString();
        }

        private HtmlTag RenderMessage(ChatMessageInput message)
        {
            var row = new HtmlTag("div").AddClass("chat-card");

            var usernameInput = message.DisplayUsernameInput;
            var avatarHtml = new DisplayUsername(usernameInput).RenderAvatar();
            row.AppendHtml($"<span class=\"chat-card-avatar\">{avatarHtml}</span>");

            var body = new HtmlTag("div").AddClass("chat-card-body");

            var metaRow = new HtmlTag("div").AddClass("chat-card-meta");

            usernameInput.ShowAvatar = false;
            metaRow.AppendHtml(new DisplayUsername(usernameInput).Render());
            metaRow.AppendHtml(new TimeSince(message.TimeSinceCreated).Render());
            body.Append(metaRow);

            var text = new HtmlTag("div").AddClass("chat-card-text");
            text.AppendHtml(message.MessageText);
            body.Append(text);

            row.Append(body);

            if (message.UserCanDelete)
            {
                var deleteBtn = new HtmlTag("button")
                    .AddClass("chat-card-delete")
                    .Attr("type", "button")
                    .Attr("data-messageid", message.MessageId.ToString())
                    .Attr("onclick", "DeleteChat(this)")
                    .Attr("aria-label", "Delete message")
                    .AppendHtml(new Icon(new IconInput { Type = IconType.Trash, Size = 15, Color = "#ef4444" }).Render());
                row.Append(deleteBtn);
            }

            return row;
        }
    }
}