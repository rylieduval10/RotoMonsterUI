using System;

namespace RotoMonsterUI
{
    public static class ColorHelper
    {
        public const string Black = "000000";
        public const string White = "FFFFFF";

        private static string GetColorCode(int r, int g, int b)
        {
            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        private static double GetPercentFloat(float value, float low, float high, bool colorHigh)
        {
            if (high == low) return 0;
            double percent = (double)(value - low) / (high - low) * 100;
            percent = Math.Max(0, Math.Min(100, percent));
            return colorHigh ? percent : 100 - percent;
        }

        private static double GetPercent(int value, int low, int high, bool colorHigh)
        {
            return GetPercentFloat((float)value, (float)low, (float)high, colorHigh);
        }

        // Float versions (real implementation)
        public static string GetYellowColorCode(float value, float low, float high, bool colorHigh)
        {
            double percent = GetPercentFloat(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(255, 255, otherColor);
        }

        public static string GetRedColorCode(float value, float low, float high, bool colorHigh)
        {
            double percent = GetPercentFloat(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(255, otherColor, otherColor);
        }

        public static string GetGreenColorCode(float value, float low, float high, bool colorHigh)
        {
            double percent = GetPercentFloat(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(otherColor, 255, otherColor);
        }

        public static string GetCyanColorCode(float value, float low, float high, bool colorHigh)
        {
            double percent = GetPercentFloat(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent * 0.8, 0);
            return GetColorCode(otherColor, 255, 255);
        }

        // Int versions (call through to float)
        public static string GetYellowColorCode(int value, int low, int high, bool colorHigh)
            => GetYellowColorCode((float)value, (float)low, (float)high, colorHigh);

        public static string GetRedColorCode(int value, int low, int high, bool colorHigh)
            => GetRedColorCode((float)value, (float)low, (float)high, colorHigh);

        public static string GetGreenColorCode(int value, int low, int high, bool colorHigh)
            => GetGreenColorCode((float)value, (float)low, (float)high, colorHigh);

        public static string GetCyanColorCode(int value, int low, int high, bool colorHigh)
            => GetCyanColorCode((float)value, (float)low, (float)high, colorHigh);
    }
}