using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class PollService
    {
        public PollResult Process(int pollId, List<int> optionIds, List<int> currentlySelectedOptionIds, Dictionary<string, string> params_)
        {
            var result = new PollResult();

            if (params_.ContainsKey($"clearpollvote_{pollId}"))
                result.ClearVotePressed = true;

            if (params_.ContainsKey($"deletepoll_{pollId}"))
                result.DeletePollPressed = true;

            foreach (var optionId in optionIds)
            {
                if (params_.ContainsKey($"choosepoll_{pollId}_{optionId}"))
                {
                    var wouldSelectAll = currentlySelectedOptionIds.Count == optionIds.Count - 1
                        && !currentlySelectedOptionIds.Contains(optionId);

                    if (!wouldSelectAll)
                        result.ChosenOptionId = optionId;
                }

                if (params_.ContainsKey($"editpollplayers_{pollId}_{optionId}"))
                    result.ToggleEditPlayersOptionId = optionId;

                var pickerId = $"poll-{pollId}-option-{optionId}-players";

                if (params_.ContainsKey($"{pickerId}-add"))
                {
                    result.AddPlayerOptionId = optionId;
                    if (params_.TryGetValue($"{pickerId}-selected", out var selectedPlayerId)
                        && int.TryParse(selectedPlayerId, out var parsedId))
                    {
                        result.SelectedPlayerIdToAdd = parsedId;
                    }
                }

                var removePrefix = $"{pickerId}-remove-";
                var removeKey = params_.Keys.FirstOrDefault(k => k.StartsWith(removePrefix));
                if (removeKey != null && int.TryParse(removeKey.Substring(removePrefix.Length), out var removePlayerId))
                {
                    result.RemovePlayerOptionId = optionId;
                    result.RemovePlayerId = removePlayerId;
                }
            }

            return result;
        }
    }
}