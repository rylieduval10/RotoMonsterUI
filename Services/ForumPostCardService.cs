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

            result.DeletePostId = ExtractId("forumdelete_");
            result.EditPostId = ExtractId("forumedit_");

            var voteResult = new VoteControlService().Process("forum", params_);
            result.UpVotePostId = voteResult.UpVoteId;
            result.DownVotePostId = voteResult.DownVoteId;
            result.CancelVotePostId = voteResult.CancelVoteId;

            return result;
        }
    }
}