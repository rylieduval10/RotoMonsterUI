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

        public static string GetTeamColorVar(string teamCode)
        {
            if (string.IsNullOrEmpty(teamCode)) return null;
            if (TeamColors.ContainsKey(teamCode.ToUpper()))
                return $"var(--team-mlb-{teamCode.ToLower()})";
            return "#64748b";
        }

        private static readonly Dictionary<string, (string Light, string Dark)> NbaTeamColors = new Dictionary<string, (string, string)>
        {
            { "ATL", ("#E14434", "#EF988F") },
            { "BOS", ("#007A33", "#73B68F") },
            { "BKN", ("#000000", "#737373") },
            { "CHA", ("#00788C", "#73B5C0") },
            { "CHI", ("#CE1141", "#E47C97") },
            { "CLE", ("#860038", "#BC7392") },
            { "DAL", ("#0053BC", "#73A0DA") },
            { "DEN", ("#24467E", "#4271D7") },
            { "DET", ("#C8102E", "#E17C8C") },
            { "GSW", ("#1D428A", "#8397BF") },
            { "GS",  ("#1D428A", "#8397BF") },
            { "HOU", ("#CE1141", "#E47C97") },
            { "IND", ("#002D62", "#2781E7") },
            { "LAC", ("#C8102E", "#E17C8C") },
            { "LAL", ("#552582", "#A287BA") },
            { "MEM", ("#5D76A9", "#A6B4D0") },
            { "MIA", ("#98002E", "#C6738C") },
            { "MIL", ("#00471B", "#739A82") },
            { "MIN", ("#154C8C", "#4271D7") },
            { "NOP", ("#1B3F7A", "#2B6CEE") },
            { "NO",  ("#1B3F7A", "#2B6CEE") },
            { "NYK", ("#006BB6", "#73AED7") },
            { "NY",  ("#006BB6", "#73AED7") },
            { "OKC", ("#007DC3", "#73B8DE") },
            { "ORL", ("#007DC5", "#73B8DF") },
            { "PHI", ("#006BB6", "#73AED7") },
            { "PHX", ("#3D2E8C", "#837CA8") },
            { "PHO", ("#3D2E8C", "#837CA8") },
            { "POR", ("#E03A3E", "#EE9395") },
            { "SAC", ("#5B2B82", "#A58ABA") },
            { "SAS", ("#C4CED3", "#C4CED3") },
            { "SA",  ("#C4CED3", "#C4CED3") },
            { "TOR", ("#CE1141", "#E47C97") },
            { "UTA", ("#154C8C", "#2B86EE") },
            { "UTAH", ("#154C8C", "#2B86EE") },
            { "WAS", ("#154C8C", "#2B86EE") },
        };

        public static string GetNbaTeamColor(string teamCode, bool isDarkMode = false)
        {
            if (string.IsNullOrEmpty(teamCode)) return null;
            if (NbaTeamColors.TryGetValue(teamCode.ToUpper(), out var colors))
                return isDarkMode ? colors.Dark : colors.Light;
            return "#64748b";
        }

        public static string GetNbaTeamColorVar(string teamCode)
        {
            if (string.IsNullOrEmpty(teamCode)) return null;
            if (NbaTeamColors.ContainsKey(teamCode.ToUpper()))
                return $"var(--team-nba-{teamCode.ToLower()})";
            return "#64748b";
        }
        
    }
}