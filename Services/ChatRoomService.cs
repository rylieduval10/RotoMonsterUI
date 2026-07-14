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

            foreach (var key in formValues.Keys)
            {
                if (key.StartsWith($"deletemsg_{id}_") && int.TryParse(key.Substring($"deletemsg_{id}_".Length), out var msgId))
                {
                    result.DeleteMessageId = msgId;
                    break;
                }
            }

            return result;
        }
    }
}