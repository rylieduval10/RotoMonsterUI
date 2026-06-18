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
            var arrowColor = _arrowColor ?? "#FFFFFF";
            var stroke = _strokeColor ?? "none";
            return $@"<svg width=""{_size}"" height=""{_size}"" viewBox=""0 0 48 48"" xmlns=""http://www.w3.org/2000/svg"" style=""display:inline-block;"">
            <path d=""M24 46 L4 22 A24 24 0 0 1 44 22 Z"" fill=""{_color}"" stroke=""{stroke}"" stroke-width=""2""/>
            <g transform=""rotate({_degrees}, 24, 28)"">
                <line x1=""24"" y1=""36"" x2=""24"" y2=""24"" stroke=""{arrowColor}"" stroke-width=""2"" stroke-linecap=""round""/>
                <polygon points=""24,20 20,26 28,26"" fill=""{arrowColor}""/>
            </g>
        </svg>";
        }
    }
}