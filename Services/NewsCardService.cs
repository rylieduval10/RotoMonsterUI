using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class NewsCardService
    {

        private static readonly string[] KeyPrefixes =
        {
            "editnews_", "deletenews_", "savenews_", "cancelnews_", "settag_",
            "status_", "tag_", "newstitle_", "source_", "newsdetails_", "unofficial_", "level_", "newstag_"
        };

        public static int? GetActiveNewsId(Dictionary<string, string> params_)
        {
            if (params_.TryGetValue("__EVENTTARGET", out var eventTarget) && eventTarget != null)
            {
                foreach (var prefix in new[] { "editnews_", "deletenews_" })
                {
                    if (eventTarget.StartsWith(prefix) && int.TryParse(eventTarget.Substring(prefix.Length), out var idFromTarget))
                        return idFromTarget;
                }
            }

            foreach (var key in params_.Keys)
            {
                foreach (var prefix in KeyPrefixes)
                {
                    if (!key.StartsWith(prefix)) continue;
                    var rest = key.Substring(prefix.Length);
                    var idPart = prefix == "newstag_" ? rest.Split('_')[0] : rest;

                    if (int.TryParse(idPart, out var id))
                        return id;
                }
            }

            return null;
        }

        public NewsCardResult Process(Dictionary<string, string> params_)
        {
            var newsId = GetActiveNewsId(params_);
            return newsId.HasValue ? Process(newsId.Value, params_) : new NewsCardResult();
        }

        public static int? GetDeletedNewsId(Dictionary<string, string> params_)
        {
            if (params_.TryGetValue("__EVENTTARGET", out var eventTarget) &&
                eventTarget != null &&
                eventTarget.StartsWith("deletenews_") &&
                int.TryParse(eventTarget.Substring("deletenews_".Length), out var newsId))
            {
                return newsId;
            }
            return null;
        }

        public NewsCardResult Process(int newsId, Dictionary<string, string> params_)
        {
            var result = new NewsCardResult();

            var editKey = $"editnews_{newsId}";

            var editViaFormKey = params_.ContainsKey(editKey);
            var editViaEventTarget = params_.TryGetValue("__EVENTTARGET", out var eventTarget) && eventTarget == editKey;

            if (editViaFormKey || editViaEventTarget)
                result.ToggleEditNewsId = newsId;

            var deleteKey = $"deletenews_{newsId}";
            if (params_.ContainsKey(deleteKey) || (eventTarget != null && eventTarget == deleteKey))
                result.DeleteNewsId = newsId;

            if (params_.ContainsKey($"savenews_{newsId}"))
            {
                result.SavePressed = true;
                result.SaveNewsId = newsId;
            }

            if (params_.ContainsKey($"cancelnews_{newsId}"))
            {
                result.CancelPressed = true;
                result.CancelNewsId = newsId;
            }

            if (params_.ContainsKey($"settag_{newsId}"))
            {
                result.SetTagPressed = true;
                result.SetTagNewsId = newsId;
            }

            if (params_.TryGetValue($"status_{newsId}", out var status))
                result.StatusTypeText = status;

            if (params_.TryGetValue($"tag_{newsId}", out var tag))
                result.StatusTypeTag = tag;

            if (params_.TryGetValue($"newstitle_{newsId}", out var title))
                result.NewsTitle = title;

            if (params_.TryGetValue($"source_{newsId}", out var source))
                result.SourceURL = source;

            if (params_.TryGetValue($"newsdetails_{newsId}", out var details))
                result.NewsDetails = details;

            result.IsUnofficial = params_.ContainsKey($"unofficial_{newsId}");

            if (params_.TryGetValue($"level_{newsId}", out var level) && System.Enum.TryParse<NewsLevel>(level, true, out var parsedLevel))
                result.NewsLevel = parsedLevel;

            var tagPrefix = $"newstag_{newsId}_";
            result.CheckedNewsTagIds = params_.Keys
                .Where(k => k.StartsWith(tagPrefix))
                .Select(k => int.TryParse(k.Substring(tagPrefix.Length), out var id) ? id : -1)
                .Where(id => id >= 0)
                .ToList();

            return result;
        }
    }
}