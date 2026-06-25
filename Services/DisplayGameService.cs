using System.Collections.Generic;

namespace RotoMonsterUI
{
    public class DisplayGamesResult
    {
        public List<int> SelectedGameIds { get; set; } = new List<int>();
    }

    public class DisplayGamesService
    {
        public DisplayGamesResult Process(string controlId, List<int> gameIds, Dictionary<string, string> formValues)
        {
            var result = new DisplayGamesResult();

            foreach (var gameId in gameIds)
            {
                var key = $"{controlId}-game-{gameId}";
                if (formValues.ContainsKey(key))
                    result.SelectedGameIds.Add(gameId);
            }

            return result;
        }
    }
}