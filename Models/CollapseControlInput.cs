namespace RotoMonsterUI
{
    public class CollapseControlInput
    {
        public string Id { get; set; }
        public string ButtonText { get; set; }
        public ButtonStyle ButtonStyle { get; set; } = ButtonStyle.Secondary;
        public string CollapsibleHtml { get; set; }
        public bool IsExpanded { get; set; } = false;
    }
}