using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PositionFilterControlResult
    {
        public bool IsAllButtonPressed { get; set; }
        public bool IsClearButtonPressed { get; set; }
        public List<int> SelectedPositionIds { get; set; } = new List<int>();
    }

    public class PositionFilterControlService
    {
        public PositionFilterControlResult Process(string controlId, Dictionary<string, string> formValues)
        {
            var result = new PositionFilterControlResult();

            if (formValues.ContainsKey($"{controlId}-all"))
                result.IsAllButtonPressed = true;

            if (formValues.ContainsKey($"{controlId}-clear"))
                result.IsClearButtonPressed = true;

            foreach (var kvp in formValues)
            {
                if (kvp.Key.StartsWith("pos_") && int.TryParse(kvp.Value, out int posId))
                    result.SelectedPositionIds.Add(posId);
            }

            return result;
        }
    }
}