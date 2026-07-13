using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class NewsCardResult
    {

        public int? ToggleEditNewsId { get; set; }

        public int? DeleteNewsId { get; set; }

        public bool SavePressed { get; set; }
        public int? SaveNewsId { get; set; }

        public bool CancelPressed { get; set; }
        public int? CancelNewsId { get; set; }

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