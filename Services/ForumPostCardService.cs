using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class ForumPostCardService
    {
        public ForumPostCardResult Process(Dictionary<string, string> params_)
        {
            var result = new ForumPostCardResult();

            foreach (var key in params_.Keys)
            {
                if (key.StartsWith("forumupvote_") && int.TryParse(key.Replace("forumupvote_", ""), out int upId))
                    result.UpVotePostId = upId;

                else if (key.StartsWith("forumdownvote_") && int.TryParse(key.Replace("forumdownvote_", ""), out int downId))
                    result.DownVotePostId = downId;

                else if (key.StartsWith("forumdelete_") && int.TryParse(key.Replace("forumdelete_", ""), out int deleteId))
                    result.DeletePostId = deleteId;

                else if (key.StartsWith("forumedit_") && int.TryParse(key.Replace("forumedit_", ""), out int editId))
                    result.EditPostId = editId;
            }

            return result;
        }
    }
}