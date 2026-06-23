namespace RotoMonsterUI
{
    public class ScoringCategory
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string ColorCSS { get; set; } // e.g. "var(--color-hitter-category)" or "var(--color-pitcher-category)"
    }
}