namespace RotoMonsterUI
{
    public class Icon
    {
        private IconInput _input;

        public Icon(IconInput input)
        {
            _input = input;
        }

        public Icon WithSize(int size) { _input.Size = size; return this; }
        public Icon WithColor(string color) { _input.Color = color; return this; }

        private string WrapSvg(string innerPath)
        {
            return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 24 24"" fill=""{_input.Fill}"" stroke=""{_input.Color}"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round"">{innerPath}</svg>";
        }

        public string Render()
        {
            switch (_input.Type)
            {
                case IconType.Settings:
                    return WrapSvg(@"<path d=""M12.22 2h-.44a2 2 0 0 0-2 2v.18a2 2 0 0 1-1 1.73l-.43.25a2 2 0 0 1-2 0l-.15-.08a2 2 0 0 0-2.73.73l-.22.38a2 2 0 0 0 .73 2.73l.15.1a2 2 0 0 1 1 1.72v.51a2 2 0 0 1-1 1.74l-.15.09a2 2 0 0 0-.73 2.73l.22.38a2 2 0 0 0 2.73.73l.15-.08a2 2 0 0 1 2 0l.43.25a2 2 0 0 1 1 1.73V20a2 2 0 0 0 2 2h.44a2 2 0 0 0 2-2v-.18a2 2 0 0 1 1-1.73l.43-.25a2 2 0 0 1 2 0l.15.08a2 2 0 0 0 2.73-.73l.22-.39a2 2 0 0 0-.73-2.73l-.15-.08a2 2 0 0 1-1-1.74v-.5a2 2 0 0 1 1-1.74l.15-.09a2 2 0 0 0 .73-2.73l-.22-.38a2 2 0 0 0-2.73-.73l-.15.08a2 2 0 0 1-2 0l-.43-.25a2 2 0 0 1-1-1.73V4a2 2 0 0 0-2-2z""/><circle cx=""12"" cy=""12"" r=""3""/>");
                case IconType.RefreshRosters:
                    return WrapSvg(@"<path d=""M3 12a9 9 0 0 1 9-9 9.75 9.75 0 0 1 6.74 2.74L21 8""/><path d=""M21 3v5h-5""/><path d=""M21 12a9 9 0 0 1-9 9 9.75 9.75 0 0 1-6.74-2.74L3 16""/><path d=""M8 16H3v5""/>");
                case IconType.PostponementChanceWarning:
                    return WrapSvg(@"<path d=""M4 15s1-1 4-1 5 2 8 2 4-1 4-1V3s-1 1-4 1-5-2-8-2-4 1-4 1z""/><line x1=""4"" y1=""22"" x2=""4"" y2=""15""/>");
                case IconType.Info:
                    return WrapSvg(@"<circle cx=""12"" cy=""12"" r=""10""/><path d=""M12 16v-4""/><path d=""M12 8h.01""/>");
                case IconType.Dome:
                    return WrapSvg(@"<path d=""M2 22 L2 14 A10 8 0 0 1 22 14 L22 22 L2 22"" stroke-linejoin=""round""/>");

                case IconType.RetractableDome:
    return WrapSvg(@"<line x1=""4"" y1=""16"" x2=""20"" y2=""16""/><line x1=""4"" y1=""16"" x2=""4"" y2=""11""/><line x1=""20"" y1=""16"" x2=""20"" y2=""11""/><path d=""M4 11 A9 9 0 0 1 8.5 4.5""/><path d=""M20 11 A9 9 0 0 0 15.5 4.5""/><path d=""M7 11 A6 6 0 0 1 10 7"" stroke-width=""1""/><path d=""M17 11 A6 6 0 0 0 14 7"" stroke-width=""1""/>");
                default:
                    return "";
            }
        }
    }
}