using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ForumPostCardService
    {
        public ForumPostCardResult Process(Dictionary<string, string> params_)
        {
            var result = new ForumPostCardResult();

            params_.TryGetValue("__EVENTTARGET", out var eventTarget);

            int? ExtractId(string prefix)
            {
                if (eventTarget != null && eventTarget.StartsWith(prefix) &&
                    int.TryParse(eventTarget.Substring(prefix.Length), out var idFromTarget))
                    return idFromTarget;

                foreach (var key in params_.Keys)
                {
                    if (key.StartsWith(prefix) && int.TryParse(key.Substring(prefix.Length), out var idFromKey))
                        return idFromKey;
                }
                return null;
            }

            var upId = ExtractId("forumupvote_");
            var downId = ExtractId("forumdownvote_");
            result.DeletePostId = ExtractId("forumdelete_");
            result.EditPostId = ExtractId("forumedit_");

            if (upId.HasValue)
            {
                var currentVote = params_.TryGetValue($"forumcurrentvote_{upId.Value}", out var cv) ? cv : "none";
                if (currentVote == "up")
                    result.CancelVotePostId = upId;
                else
                    result.UpVotePostId = upId;
            }

            if (downId.HasValue)
            {
                var currentVote = params_.TryGetValue($"forumcurrentvote_{downId.Value}", out var cv) ? cv : "none";
                if (currentVote == "down")
                    result.CancelVotePostId = downId;
                else
                    result.DownVotePostId = downId;
            }

            return result;
        }
    }
}