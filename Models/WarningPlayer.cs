using System.Collections.Generic;

namespace RotoMonsterUI
{
    public enum PlayerWarningType
    {
        Warning,
        Alert
    }

    public class WarningPlayer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeamCode { get; set; }
        public List<FilterPosition> Positions { get; set; } = new List<FilterPosition>();
    }
}