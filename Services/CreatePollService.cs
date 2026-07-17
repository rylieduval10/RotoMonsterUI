using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class CreatePollService
    {
        public CreatePollResult Process(List<int> optionIds, Dictionary<string, string> params_)
        {
            var result = new CreatePollResult();

            if (params_.ContainsKey("create-poll-submit"))
                result.CreatePollPressed = true;

            if (params_.ContainsKey("create-poll-refresh-rosters"))
                result.RefreshRostersPressed = true;

            if (params_.TryGetValue("create-poll-league", out var leagueId))
                result.SelectedLeagueId = leagueId;

            if (params_.TryGetValue("create-poll-settings", out var settingsId))
                result.SelectedLeagueSettingsId = settingsId;

            if (params_.TryGetValue("create-poll-question", out var question))
                result.Question = question;

            if (params_.TryGetValue("create-poll-expires-hours", out var expiresStr) && int.TryParse(expiresStr, out var expiresHours))
                result.ExpiresInHours = expiresHours;

            if (params_.ContainsKey("create-poll-add-option"))
                result.AddOptionPressed = true;

            var removeOptionKey = params_.Keys.FirstOrDefault(k => k.StartsWith("create-poll-remove-option-"));
            if (removeOptionKey != null && int.TryParse(removeOptionKey.Substring("create-poll-remove-option-".Length), out var removeOptionId))
                result.RemoveOptionId = removeOptionId;

            var moveUpKey = params_.Keys.FirstOrDefault(k => k.StartsWith("create-poll-moveup-option-"));
            if (moveUpKey != null && int.TryParse(moveUpKey.Substring("create-poll-moveup-option-".Length), out var moveUpId))
                result.MoveUpOptionId = moveUpId;

            var moveDownKey = params_.Keys.FirstOrDefault(k => k.StartsWith("create-poll-movedown-option-"));
            if (moveDownKey != null && int.TryParse(moveDownKey.Substring("create-poll-movedown-option-".Length), out var moveDownId))
                result.MoveDownOptionId = moveDownId;

            foreach (var optionId in optionIds)
            {
                if (params_.TryGetValue($"create-poll-option-{optionId}-comment", out var comment))
                    result.OptionComments[optionId] = comment;

                var pickerId = $"create-poll-option-{optionId}-players";

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