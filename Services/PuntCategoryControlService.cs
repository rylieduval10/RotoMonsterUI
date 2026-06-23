using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PuntCategoryResult
    {
        public List<int> SelectedIds { get; set; } = new List<int>();
    }

    public class PuntCategoryService
    {
        public PuntCategoryResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new PuntCategoryResult();

            foreach (var kvp in formValues)
            {
                if (kvp.Key.StartsWith("cat_") && int.TryParse(kvp.Value, out int id))
                    result.SelectedIds.Add(id);
            }

            return result;
        }
    }
}