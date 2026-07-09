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
                case IconType.PersonSimple:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 60 60"">
                        <circle cx=""30"" cy=""10"" r=""8"" fill=""{_input.Color}""/>
                        <line x1=""30"" y1=""18"" x2=""30"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""16"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""44"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""20"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""40"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                    </svg>";

                case IconType.PersonAlert:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 60 60"">
                        <circle cx=""26"" cy=""10"" r=""8"" fill=""{_input.Color}""/>
                        <line x1=""26"" y1=""18"" x2=""26"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""27"" x2=""12"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""27"" x2=""40"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""40"" x2=""16"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""40"" x2=""36"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <circle cx=""48"" cy=""48"" r=""11"" fill=""{_input.Color}"" stroke=""white"" stroke-width=""2.5""/>
                        <text x=""48"" y=""53"" text-anchor=""middle"" font-size=""13"" font-weight=""900"" fill=""white"" font-family=""sans-serif"">!</text>
                    </svg>";

                case IconType.PersonConfirmed:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 60 60"">
                        <circle cx=""26"" cy=""10"" r=""8"" fill=""{_input.Color}""/>
                        <line x1=""26"" y1=""18"" x2=""26"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""27"" x2=""12"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""27"" x2=""40"" y2=""36"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""40"" x2=""16"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""26"" y1=""40"" x2=""36"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <circle cx=""48"" cy=""48"" r=""11"" fill=""{_input.Color}"" stroke=""white"" stroke-width=""2.5""/>
                        <text x=""48"" y=""54"" text-anchor=""middle"" font-size=""13"" font-weight=""900"" fill=""white"" font-family=""sans-serif"">✓</text>
                    </svg>";

                case IconType.PersonArmsDown:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 60 60"">
                        <circle cx=""30"" cy=""10"" r=""8"" fill=""{_input.Color}""/>
                        <line x1=""30"" y1=""18"" x2=""30"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""16"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""44"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""20"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""40"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                    </svg>";

                case IconType.PersonArmsUp:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 60 60"">
                        <circle cx=""30"" cy=""10"" r=""8"" fill=""{_input.Color}""/>
                        <line x1=""30"" y1=""18"" x2=""30"" y2=""40"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""16"" y2=""14"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""27"" x2=""44"" y2=""14"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""20"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                        <line x1=""30"" y1=""40"" x2=""40"" y2=""55"" stroke=""{_input.Color}"" stroke-width=""4"" stroke-linecap=""round""/>
                    </svg>";
                case IconType.Settings:
                    return WrapSvg(@"<path d=""M12.22 2h-.44a2 2 0 0 0-2 2v.18a2 2 0 0 1-1 1.73l-.43.25a2 2 0 0 1-2 0l-.15-.08a2 2 0 0 0-2.73.73l-.22.38a2 2 0 0 0 .73 2.73l.15.1a2 2 0 0 1 1 1.72v.51a2 2 0 0 1-1 1.74l-.15.09a2 2 0 0 0-.73 2.73l.22.38a2 2 0 0 0 2.73.73l.15-.08a2 2 0 0 1 2 0l.43.25a2 2 0 0 1 1 1.73V20a2 2 0 0 0 2 2h.44a2 2 0 0 0 2-2v-.18a2 2 0 0 1 1-1.73l.43-.25a2 2 0 0 1 2 0l.15.08a2 2 0 0 0 2.73-.73l.22-.39a2 2 0 0 0-.73-2.73l-.15-.08a2 2 0 0 1-1-1.74v-.5a2 2 0 0 1 1-1.74l.15-.09a2 2 0 0 0 .73-2.73l-.22-.38a2 2 0 0 0-2.73-.73l-.15.08a2 2 0 0 1-2 0l-.43-.25a2 2 0 0 1-1-1.73V4a2 2 0 0 0-2-2z""/><circle cx=""12"" cy=""12"" r=""3""/>");
                case IconType.RefreshRosters:
                    return WrapSvg(@"<path d=""M3 12a9 9 0 0 1 9-9 9.75 9.75 0 0 1 6.74 2.74L21 8""/><path d=""M21 3v5h-5""/><path d=""M21 12a9 9 0 0 1-9 9 9.75 9.75 0 0 1-6.74-2.74L3 16""/><path d=""M8 16H3v5""/>");
                case IconType.PostponementChanceWarning:
                    return WrapSvg(@"<path d=""M4 15s1-1 4-1 5 2 8 2 4-1 4-1V3s-1 1-4 1-5-2-8-2-4 1-4 1z""/><line x1=""4"" y1=""22"" x2=""4"" y2=""15""/>");
                case IconType.Info:
                    return WrapSvg(@"<circle cx=""12"" cy=""12"" r=""10""/><path d=""M12 16v-4""/><path d=""M12 8h.01""/>");
               case IconType.Dome:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 24 24"" fill=""none"" stroke=""{_input.Color}"" stroke-width=""2.5"" stroke-linecap=""round"" stroke-linejoin=""round"">
                        <path d=""M2 22 L2 14 A10 8 0 0 1 22 14 L22 22 L2 22"" fill=""{_input.Fill}"" stroke=""{_input.Color}""/>
                    </svg>";
                case IconType.Trash:
                    return WrapSvg(@"<polyline points=""3 6 5 6 21 6""/><path d=""M19 6l-1 14a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2L5 6""/><path d=""M10 11v6""/><path d=""M14 11v6""/><path d=""M9 6V4a1 1 0 0 1 1-1h4a1 1 0 0 1 1 1v2""/>");
                case IconType.Next:
                    return WrapSvg(@"<polyline points=""9 18 15 12 9 6""/>");
                case IconType.Previous:
                    return WrapSvg(@"<polyline points=""15 18 9 12 15 6""/>");
                case IconType.LineupCard:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 24 24"">
                        <rect x=""2"" y=""1"" width=""20"" height=""22"" rx=""4"" fill=""{_input.Color}""/>
                        <line x1=""6"" y1=""7"" x2=""18"" y2=""7"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round""/>
                        <line x1=""6"" y1=""11"" x2=""18"" y2=""11"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round""/>
                        <line x1=""6"" y1=""15"" x2=""18"" y2=""15"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round""/>
                        <line x1=""6"" y1=""19"" x2=""13"" y2=""19"" stroke=""white"" stroke-width=""2"" stroke-linecap=""round""/>
                    </svg>";

                   case IconType.Illness:
                        return WrapSvg(@"<rect x=""7"" y=""9"" width=""10"" height=""12"" rx=""2""/><rect x=""9"" y=""4"" width=""6"" height=""5"" rx=""1""/><path d=""M9 6h6""/><path d=""M7 14h10""/>");
                case IconType.Personal:
                    return WrapSvg(@"<path d=""M3 10.5 12 3l9 7.5""/><path d=""M5 9.5V20a1 1 0 0 0 1 1h4v-6h4v6h4a1 1 0 0 0 1-1V9.5""/>");
                case IconType.CoachsDecision:
                    return WrapSvg(@"<rect x=""6"" y=""4"" width=""12"" height=""17"" rx=""2""/><rect x=""9"" y=""2"" width=""6"" height=""4"" rx=""1""/><path d=""M9 11h6""/><path d=""M9 15h6""/>");
                case IconType.Dental:
                    return WrapSvg(@"<path d=""M12 21c-.6 0-1-1.4-1.4-3.4-.3-1.6-.6-2.3-1.1-2.3s-.8.7-1.1 2.3C8 19.6 7.6 21 7 21c-1.8 0-2.7-2.7-3.2-5.8C3.4 12.6 3 9.9 3 8c0-2.8 1.9-4.3 4.2-4.3 1 0 1.9.5 2.8 1 .9-.5 1.8-1 2.8-1C15.1 3.7 17 5.2 17 8c0 1.9-.4 4.6-.8 7.2-.5 3.1-1.4 5.8-3.2 5.8-.6 0-1-1.4-1.3-3-.1-.6-.3-1.1-.5-1.4""/>");
                case IconType.PossibleSuspension:
                    return WrapSvg(@"<rect x=""5"" y=""11"" width=""14"" height=""10"" rx=""2""/><path d=""M8 11V7a4 4 0 0 1 8 0v4""/>");
                case IconType.Other:
                    return WrapSvg(@"<path d=""M12 3l8 4.5v9L12 21l-8-4.5v-9L12 3z""/><path d=""M12 12v9""/><path d=""M12 12l8-4.5""/><path d=""M12 12l-8-4.5""/>");
                case IconType.Contract:
                    return WrapSvg(@"<path d=""M14 3H7a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2V8l-5-5z""/><path d=""M14 3v5h5""/><path d=""M9 13h6""/><path d=""M9 17h4""/>");
                case IconType.InjuryMaintenance:
                    return WrapSvg(@"<rect x=""3"" y=""8"" width=""18"" height=""12"" rx=""2""/><path d=""M8 8V6a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2""/><path d=""M12 12v4""/><path d=""M10 14h4""/>");
                case IconType.Injury:
                    return WrapSvg(@"<g transform=""rotate(45 12 12)""><path d=""M 7.5 4 L 16.5 4""/><path d=""M 9.5 4 L 9.5 12 L 12 15 L 12 21""/><path d=""M 14.5 4 L 14.5 12 L 12 15""/><path d=""M 9.5 8.5 L 14.5 8.5""/></g>");
                case IconType.Rest:
                    return WrapSvg(@"<path d=""M2 19v-6""/><path d=""M2 13h16a3 3 0 0 1 3 3v3""/><path d=""M2 17h19""/><rect x=""4"" y=""9"" width=""6"" height=""4"" rx=""1""/><path d=""M2 19v1""/><path d=""M21 19v1""/>");
                case IconType.TradePending:
                    return WrapSvg(@"<path d=""M3 6h18""/><path d=""M17 2l4 4-4 4""/><path d=""M21 18H3""/><path d=""M7 14l-4 4 4 4""/>");


                case IconType.Warning:
                    return WrapSvg(@"<path d=""M10.29 3.86 1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z""/><path d=""M12 9v4""/><path d=""M12 17h.01""/>");
                case IconType.Error:
                    return WrapSvg(@"<circle cx=""12"" cy=""12"" r=""10""/><path d=""M15 9l-6 6""/><path d=""M9 9l6 6""/>");


                case IconType.Rain:
                    return WrapSvg(@"<path d=""M4 14 C4 10 7 8 12 8 C17 8 20 10 20 14"" stroke-linecap=""round""/><line x1=""8"" y1=""18"" x2=""7"" y2=""21"" stroke-linecap=""round""/><line x1=""12"" y1=""18"" x2=""11"" y2=""21"" stroke-linecap=""round""/><line x1=""16"" y1=""18"" x2=""15"" y2=""21"" stroke-linecap=""round""/><path d=""M4 14 L20 14"" stroke-linecap=""round""/>");
                case IconType.LineupConfirmed:
                    return WrapSvg(@"<line x1=""10"" y1=""6"" x2=""21"" y2=""6""/><line x1=""10"" y1=""12"" x2=""21"" y2=""12""/><line x1=""10"" y1=""18"" x2=""21"" y2=""18""/><path d=""M4 6h1v4""/><path d=""M4 10h2""/><path d=""M6 18H4c0-1 2-2 2-3s-1-1.5-2-1""/>");
                case IconType.LineupNotConfirmed:
                    return WrapSvg(@"<line x1=""10"" y1=""6"" x2=""21"" y2=""6""/><line x1=""10"" y1=""12"" x2=""21"" y2=""12""/><line x1=""10"" y1=""18"" x2=""21"" y2=""18""/><path d=""M4 6h1v4""/><path d=""M4 10h2""/><path d=""M6 18H4c0-1 2-2 2-3s-1-1.5-2-1""/>");
                case IconType.Weather:
                    return WrapSvg(@"<path d=""M14 14.76V3.5a2.5 2.5 0 0 0-5 0v11.26a4.5 4.5 0 1 0 5 0z""/><circle cx=""11.5"" cy=""17.5"" r=""2.5"" fill=""#378ADD""/><line x1=""11.5"" y1=""14"" x2=""11.5"" y2=""8"" stroke-width=""2.5""/>");
                case IconType.ExportCSV:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 24 24"" fill=""none"" stroke=""{_input.Color}"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round"">
                        <path d=""M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z""/>
                        <polyline points=""14 2 14 8 20 8""/>
                        <text x=""12"" y=""17"" text-anchor=""middle"" font-size=""5.5"" font-weight=""bold"" stroke=""none"" fill=""{_input.Color}"" font-family=""system-ui"">CSV</text>
                    </svg>";

                case IconType.ExportExcel:
                    return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{_input.Size}"" height=""{_input.Size}"" viewBox=""0 0 24 24"" fill=""none"" stroke=""{_input.Color}"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round"">
                        <path d=""M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z""/>
                        <polyline points=""14 2 14 8 20 8""/>
                        <text x=""12"" y=""17"" text-anchor=""middle"" font-size=""5.5"" font-weight=""bold"" stroke=""none"" fill=""{_input.Color}"" font-family=""system-ui"">XLS</text>
                    </svg>";
                case IconType.Calendar:
                    return WrapSvg(@"<rect x=""3"" y=""4"" width=""18"" height=""18"" rx=""2""/><line x1=""3"" y1=""9"" x2=""21"" y2=""9""/><line x1=""8"" y1=""2"" x2=""8"" y2=""6""/><line x1=""16"" y1=""2"" x2=""16"" y2=""6""/><line x1=""7"" y1=""13"" x2=""9"" y2=""13""/><line x1=""12"" y1=""13"" x2=""14"" y2=""13""/><line x1=""17"" y1=""13"" x2=""19"" y2=""13""/><line x1=""7"" y1=""17"" x2=""9"" y2=""17""/><line x1=""12"" y1=""17"" x2=""14"" y2=""17""/>");
                case IconType.Save:
                    return WrapSvg(@"<path d=""M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z""/><polyline points=""17 21 17 13 7 13 7 21""/><polyline points=""7 3 7 8 15 8""/>");
                case IconType.RetractableDome:
                    return WrapSvg(@"<line x1=""4"" y1=""16"" x2=""20"" y2=""16""/><line x1=""4"" y1=""16"" x2=""4"" y2=""11""/><line x1=""20"" y1=""16"" x2=""20"" y2=""11""/><path d=""M4 11 A9 9 0 0 1 8.5 4.5""/><path d=""M20 11 A9 9 0 0 0 15.5 4.5""/><path d=""M7 11 A6 6 0 0 1 10 7"" stroke-width=""1""/><path d=""M17 11 A6 6 0 0 0 14 7"" stroke-width=""1""/>");
                default:
                    return "";
            }
        }
    }
}