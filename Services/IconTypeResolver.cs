using System;
using System.Linq;

namespace RotoMonsterUI
{
    public static class IconTypeResolver
    {
        public static IconType Resolve(string name, IconType fallback = IconType.Other)
        {
            if (string.IsNullOrEmpty(name)) return fallback;

            var normalized = new string(name.Where(char.IsLetterOrDigit).ToArray());

            if (Enum.TryParse<IconType>(normalized, ignoreCase: true, out var result))
                return result;

            return fallback;
        }
    }
}