using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DisplayPlayerInput
    {
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public string TeamCode { get; set; }
        public List<DisplayPosition> Positions { get; set; } = new List<DisplayPosition>();
    }
}