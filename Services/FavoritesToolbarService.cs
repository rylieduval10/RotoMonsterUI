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

            // Drag reorder: JS posts back with __EVENTTARGET = "{id}_reorder" and
            // the new comma-separated order in both the "{id}_order" hidden field
            // and __EVENTARGUMENT. Prefer the hidden field, fall back to the arg.
            if (eventTarget == $"{id}_reorder")
            {
                string orderCsv = null;
                if (params_.TryGetValue($"{id}_order", out var fromField) && !string.IsNullOrEmpty(fromField))
                    orderCsv = fromField;
                else if (params_.TryGetValue("__EVENTARGUMENT", out var fromArg) && !string.IsNullOrEmpty(fromArg))
                    orderCsv = fromArg;

                if (!string.IsNullOrEmpty(orderCsv))
                {
                    var list = new List<string>();
                    foreach (var part in orderCsv.Split(','))
                    {
                        if (!string.IsNullOrEmpty(part))
                            list.Add(part);
                    }
                    result.ReorderedPageIds = list;
                }
            }

            return result;
        }
    }
}