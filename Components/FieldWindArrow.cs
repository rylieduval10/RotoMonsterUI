namespace RotoMonsterUI
{
    public class FieldWindArrow
    {
        private int _degrees;
        private int _size;
        private string _color;

        public FieldWindArrow(int degrees)
        {
            _degrees = degrees;
            _size = 48;
            _color = "currentColor";
        }

        public FieldWindArrow WithSize(int size)
        {
            _size = size;
            return this;
        }

        public FieldWindArrow WithColor(string color)
        {
            _color = color;
            return this;
        }

        public string Render()
        {
            return $@"<svg width=""{_size}"" height=""{_size}"" viewBox=""0 0 48 48"" xmlns=""http://www.w3.org/2000/svg"" style=""display:inline-block;"">
    <g transform=""rotate({_degrees}, 24, 24)"">
        <line x1=""24"" y1=""38"" x2=""24"" y2=""12"" stroke=""{_color}"" stroke-width=""3"" stroke-linecap=""round""/>
        <polygon points=""24,6 16,18 32,18"" fill=""{_color}""/>
    </g>
</svg>";
        }
    }
}