using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PositionFilterControlInput
    {
        public string Id { get; set; }
        public string AllButtonId { get; set; }
        public string ClearButtonId { get; set; }
        public List<FilterPosition> Positions { get; set; } = new List<FilterPosition>();
        public List<int> SelectedPositionIds { get; set; } = new List<int>();
    }
}