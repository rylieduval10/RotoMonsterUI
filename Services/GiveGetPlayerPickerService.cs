using System;
using System.Collections.Generic;
using System.Linq;

namespace RotoMonsterUI
{
    public class GiveGetPlayerPickerService
    {
        public GiveGetPlayerPickerResult Process(string id, Dictionary<string, string> params_)
        {
            var result = new GiveGetPlayerPickerResult();

            ProcessSide($"{id}-give", params_,
                v => result.AddGivePlayerPressed = v,
                v => result.SelectedGivePlayerIdToAdd = v,
                v => result.RemoveGivePlayerId = v,
                v => result.ClearGivePressed = v);

            ProcessSide($"{id}-get", params_,
                v => result.AddGetPlayerPressed = v,
                v => result.SelectedGetPlayerIdToAdd = v,
                v => result.RemoveGetPlayerId = v,
                v => result.ClearGetPressed = v);

            if (params_.ContainsKey($"{id}-swap"))
                result.SwapPressed = true;

            return result;
        }

        private void ProcessSide(
            string pickerId,
            Dictionary<string, string> params_,
            Action<bool> setAddPressed,
            Action<int> setSelected,
            Action<int> setRemoveId,
            Action<bool> setClearPressed)
        {
            if (params_.ContainsKey($"{pickerId}-add"))
            {
                setAddPressed(true);
                if (params_.TryGetValue($"{pickerId}-selected", out var selectedPlayerId)
                    && int.TryParse(selectedPlayerId, out var parsedId))
                {
                    setSelected(parsedId);
                }
            }

            if (params_.ContainsKey($"{pickerId}-clear"))
                setClearPressed(true);

            var removePrefix = $"{pickerId}-remove-";
            var removeKey = params_.Keys.FirstOrDefault(k => k.StartsWith(removePrefix));
            if (removeKey != null && int.TryParse(removeKey.Substring(removePrefix.Length), out var removePlayerId))
                setRemoveId(removePlayerId);
        }
    }
}