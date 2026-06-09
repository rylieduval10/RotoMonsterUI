using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class CommentCardService
    {
        public CommentCardResult Process(Dictionary<string, string> params_)
        {
            var result = new CommentCardResult();

            foreach (var key in params_.Keys)
            {
                if (key.StartsWith("upvote_") && int.TryParse(key.Replace("upvote_", ""), out int upId))
                    result.UpVoteCommentId = upId;

                else if (key.StartsWith("downvote_") && int.TryParse(key.Replace("downvote_", ""), out int downId))
                    result.DownVoteCommentId = downId;

                else if (key.StartsWith("changevote_") && int.TryParse(key.Replace("changevote_", ""), out int changeId))
                    result.ChangeVoteCommentId = changeId;

                else if (key.StartsWith("delete_") && int.TryParse(key.Replace("delete_", ""), out int deleteId))
                    result.DeleteCommentId = deleteId;

                else if (key.StartsWith("expand_") && int.TryParse(key.Replace("expand_", ""), out int expandId))
                    result.ExpandCommentId = expandId;

                else if (key.StartsWith("post_") && int.TryParse(key.Replace("post_", ""), out int _))
                    result.PostPressed = true;

                else if (key.StartsWith("comment_"))
                    result.UserComment = params_[key];
            }

            return result;
        }
    }
}