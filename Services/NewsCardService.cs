using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class NewsCardService
    {
        public NewsCardResult Process(int newsId, Dictionary<string, string> params_)
        {
            var result = new NewsCardResult();

            var editKey = $"editnews_{newsId}";

            var editViaFormKey = params_.ContainsKey(editKey);
            var editViaEventTarget = params_.TryGetValue("__EVENTTARGET", out var eventTarget) && eventTarget == editKey;

            if (editViaFormKey || editViaEventTarget)
                result.ToggleEditNewsId = newsId;

            if (params_.ContainsKey($"deletenews_{newsId}"))
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