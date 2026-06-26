namespace RotoMonsterUI
{
    public class RichTextEditorInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Placeholder { get; set; } = "Type here...";
        public string InitialValue { get; set; } = "";
        public int MinHeight { get; set; } = 200;
    }
}