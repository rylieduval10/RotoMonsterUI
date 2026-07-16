using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class VoteControlService
    {
        public VoteControlResult Process(string namePrefix, Dictionary<string, string> params_)
        {
            var result = new VoteControlResult();

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

            var upPrefix = $"{namePrefix}up_";
            var downPrefix = $"{namePrefix}down_";
            var currentVotePrefix = $"{namePrefix}currentvote_";

            var upId = ExtractId(upPrefix);
            var downId = ExtractId(downPrefix);

            if (upId.HasValue)
            {
                var currentVote = params_.TryGetValue($"{currentVotePrefix}{upId.Value}", out var cv) ? cv : "none";
                result.UpVoteId = currentVote == "up" ? (int?)null : upId;
                result.CancelVoteId = currentVote == "up" ? upId : null;
            }

            if (downId.HasValue)
            {
                var currentVote = params_.TryGetValue($"{currentVotePrefix}{downId.Value}", out var cv) ? cv : "none";
                result.DownVoteId = currentVote == "down" ? (int?)null : downId;
                if (currentVote == "down") result.CancelVoteId = downId;
            }

            return result;
        }
    }
}