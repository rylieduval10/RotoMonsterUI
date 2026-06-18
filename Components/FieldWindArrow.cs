namespace RotoMonsterUI
{
    public class FieldWindArrow
    {
        private int _degrees;
        private int _size;
        private string _color;
        private string _arrowColor;
        private string _strokeColor;

        public FieldWindArrow(int degrees)
        {
            _degrees = degrees;
            _size = 48;
            _color = "currentColor";
            _arrowColor = "#FFFFFF";
        }

        public FieldWindArrow WithSize(int size)
        {
            _size = size;
            return this;
        }

        public FieldWindArrow WithStrokeColor(string strokeColor)
        {
            _strokeColor = strokeColor;
            return this;
        }

        public FieldWindArrow WithColor(string color)
        {
            _color = color;
            return this;
        }

        public FieldWindArrow WithArrowColor(string arrowColor)
        {
            _arrowColor = arrowColor;
            return this;
        }
        public string Render()
        {
            var arrowColor = _arrowColor ?? _color;
            return $@"<svg width=""{_size}"" height=""{_size}"" viewBox=""0 0 48 48"" xmlns=""http://www.w3.org/2000/svg"" style=""display:inline-block;"">
            <path d=""M6 10 A24 24 0 0 1 42 10"" fill=""none"" stroke=""#888780"" stroke-width=""3"" stroke-linecap=""round""/>
            <g transform=""rotate({_degrees}, 24, 28)"">
                <line x1=""24"" y1=""42"" x2=""24"" y2=""23"" stroke=""{_color}"" stroke-width=""8"" stroke-linecap=""butt""/>
<polygon points=""24,10 14,26 34,26"" fill=""{_color}""/>
            </g>
        </svg>";
        }
    }
}