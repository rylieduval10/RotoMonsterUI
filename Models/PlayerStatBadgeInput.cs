using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class PlayerStatBadgeItem
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
    }

    public class PlayerStatBadgeInput
    {
        public List<PlayerStatBadgeItem> Stats { get; set; } = new List<PlayerStatBadgeItem>();
    }
}