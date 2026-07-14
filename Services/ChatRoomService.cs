using System;
using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ChatRoomResult
    {
        public bool JoinChatPressed { get; set; }
        public bool SwitchToWatchingPressed { get; set; }
        public bool PostMessagePressed { get; set; }
        public bool InsertTeamAnalysis { get; set; }
        public int? DeleteMessageId { get; set; }
        public string PostedMessageHtml { get; set; }
    }

    public class ChatRoomService
    {
        public ChatRoomResult Process(string id, Dictionary<string, string> formValues)
        {
            var result = new ChatRoomResult();

            if (formValues.ContainsKey($"joinchat_{id}"))
                result.JoinChatPressed = true;

            if (formValues.ContainsKey($"switchwatching_{id}"))
                result.SwitchToWatchingPressed = true;

            if (formValues.ContainsKey($"postmessage_{id}"))
                result.PostMessagePressed = true;

            result.InsertTeamAnalysis = formValues.ContainsKey($"inserttA_{id}") && formValues[$"inserttA_{id}"] == "1";

            // The rich text editor posts its value Base64-encoded under "{id}-editor-value" -
            // same decoding RichTextEditorService does, since ChatRoom uses the same editor component.
            var editorFieldName = $"{id}-editor-value";
            if (formValues.TryGetValue(editorFieldName, out var rawValue) && !string.IsNullOrEmpty(rawValue))
            {
                try
                {
                    var bytes = Convert.FromBase64String(rawValue);
                    result.PostedMessageHtml = System.Text.Encoding.UTF8.GetString(bytes);
                }
                catch
                {
                    result.PostedMessageHtml = rawValue;
                }
            }

            // Delete button now fires via manual __doPostBack (DeleteChat JS function) since the
            // chat log runs under AJAX and normal button postback resolution doesn't fire there -
            // same issue and same fix as NewsCard's edit button. That means the target comes through
            // as the __EVENTTARGET value ("deletechat_123"), not as its own form key. Checking both
            // paths so this still works if the button is ever rendered as a normal postback button too.
            if (formValues.TryGetValue("__EVENTTARGET", out var eventTarget) && eventTarget.StartsWith("deletechat_"))
            {
                if (int.TryParse(eventTarget.Substring("deletechat_".Length), out var targetMsgId))
                    result.DeleteMessageId = targetMsgId;
            }
            else
            {
                foreach (var key in formValues.Keys)
                {
                    if (key.StartsWith($"deletemsg_{id}_") && int.TryParse(key.Substring($"deletemsg_{id}_".Length), out var msgId))
                    {
                        result.DeleteMessageId = msgId;
                        break;
                    }
                }
            }

            return result;
        }
    }
}