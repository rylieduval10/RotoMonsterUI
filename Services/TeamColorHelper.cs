using System.Collections.Generic;

namespace RotoMonsterUI
{
    public static class TeamColorHelper
    {
        private static readonly Dictionary<string, (string Light, string Dark)> TeamColors = new Dictionary<string, (string, string)>
        {
            // AL East
            { "BAL", ("#DF4601", "#F05A16") },
            { "BOS", ("#BD3039", "#D94050") },
            { "NYY", ("#003087", "#4A7BC4") },
            { "TB",  ("#092C5C", "#4A7BC4") },
            { "TOR", ("#134A8E", "#3A7AD4") },

            // AL Central
            { "CWS", ("#27251F", "#888780") },
            { "CHW", ("#27251F", "#888780") },
            { "CLE", ("#00385D", "#1A7AAF") },
            { "DET", ("#0C2340", "#4A7BC4") },
            { "KC",  ("#004687", "#3A7AD4") },
            { "MIN", ("#002B5C", "#4A7BC4") },

            // AL West
            { "HOU", ("#EB6E1F", "#EB6E1F") },
            { "LAA", ("#BA0021", "#D4203A") },
            { "ANA", ("#BA0021", "#D4203A") },
            { "OAK", ("#003831", "#19896E") },
            { "SEA", ("#0C2C56", "#3A7AD4") },
            { "TEX", ("#003278", "#3A7AD4") },

            // NL East
            { "ATL", ("#CE1141", "#E02255") },
            { "MIA", ("#00A3E0", "#00A3E0") },
            { "NYM", ("#002D72", "#3A7AD4") },
            { "PHI", ("#E81828", "#E81828") },
            { "WAS", ("#AB0003", "#C4101A") },
            { "WSH", ("#AB0003", "#C4101A") },

            // NL Central
            { "CHC", ("#0E3386", "#3A7AD4") },
            { "CIN", ("#C6011F", "#C6011F") },
            { "MIL", ("#12284B", "#3A7AD4") },
            { "PIT", ("#FDB827", "#FDB827") },
            { "STL", ("#C41E3A", "#C41E3A") },

            // NL West
            { "ARI", ("#A71930", "#C42240") },
            { "COL", ("#33006F", "#6A35B8") },
            { "LAD", ("#005A9C", "#1A7AD4") },
            { "SD",  ("#2F241D", "#8B6A50") },
            { "SDP", ("#2F241D", "#8B6A50") },
            { "SF",  ("#FD5A1E", "#FD5A1E") },
            { "SFG", ("#FD5A1E", "#FD5A1E") },
        };

        public static string GetTeamColor(string teamCode, bool isDarkMode = false)
        {
            if (string.IsNullOrEmpty(teamCode)) return null;
            if (TeamColors.TryGetValue(teamCode.ToUpper(), out var colors))
                return isDarkMode ? colors.Dark : colors.Light;
            return "#64748b"; 
        }
    }
}