using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class NewsCardResult
    {
        /// <summary>Set when the edit icon was clicked for this news item. Since this is now
        /// a plain button (not a checkbox), it just signals "toggle this" - flip your own
        /// stored IsEditing value for this NewsId and re-render.</summary>
        public int? ToggleEditNewsId { get; set; }

        public int? DeleteNewsId { get; set; }

        public bool SavePressed { get; set; }
        public int? SaveNewsId { get; set; }

        public bool SetTagPressed { get; set; }
        public int? SetTagNewsId { get; set; }

        public string StatusTypeText { get; set; }
        public string StatusTypeTag { get; set; }
        public string NewsTitle { get; set; }
        public string SourceURL { get; set; }
        public string NewsDetails { get; set; }
        public bool IsUnofficial { get; set; }
        public NewsLevel? NewsLevel { get; set; }
        public List<int> CheckedNewsTagIds { get; set; } = new List<int>();
    }
}