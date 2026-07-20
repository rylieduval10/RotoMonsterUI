using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class FavoritesToolbarService
    {
        public FavoritesToolbarResult Process(string id, Dictionary<string, string> params_)
        {
            var result = new FavoritesToolbarResult();

            params_.TryGetValue("__EVENTTARGET", out var eventTarget);

            string ExtractPageId(string prefix)
            {
                if (eventTarget != null && eventTarget.StartsWith(prefix))
                    return eventTarget.Substring(prefix.Length);

                foreach (var key in params_.Keys)
                {
                    if (key.StartsWith(prefix))
                        return key.Substring(prefix.Length);
                }
                return null;
            }

            var hidePrefix = $"{id}_hide_";

            result.HidePageId = ExtractPageId(hidePrefix);

            result.AddPageId = ExtractPageId($"{id}_addcurrent_");

            return result;
        }
    }
}