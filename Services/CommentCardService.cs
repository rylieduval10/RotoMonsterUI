using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CommentCardService
    {
        public CommentCardResult Process(Dictionary<string, string> params_)
        {
            var result = new CommentCardResult();

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

            var upId = ExtractId("commentupvote_");
            var downId = ExtractId("commentdownvote_");
            result.DeleteCommentId = ExtractId("delete_");
            result.ExpandCommentId = ExtractId("expand_");

            if (upId.HasValue)
            {
                var currentVote = params_.TryGetValue($"commentcurrentvote_{upId.Value}", out var cv) ? cv : "none";
                if (currentVote == "up")
                    result.CancelVoteCommentId = upId;
                else
                    result.UpVoteCommentId = upId;
            }

            if (downId.HasValue)
            {
                var currentVote = params_.TryGetValue($"commentcurrentvote_{downId.Value}", out var cv) ? cv : "none";
                if (currentVote == "down")
                    result.CancelVoteCommentId = downId;
                else
                    result.DownVoteCommentId = downId;
            }

            foreach (var key in params_.Keys)
            {
                if (key.StartsWith("post_") && int.TryParse(key.Replace("post_", ""), out int postId))
                {
                    result.PostPressed = true;
                    result.PostCommentId = postId;
                }
                else if (key.StartsWith("comment_"))
                    result.UserComment = params_[key];
            }

            return result;
        }
    }
}