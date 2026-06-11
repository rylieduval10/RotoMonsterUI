using System;

namespace RotoMonsterUI
{
    public static class ColorHelper
    {
        private static string GetColorCode(int r, int g, int b)
        {
            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }

        private static double GetPercent(int value, int low, int high, bool colorHigh)
        {
            if (high == low) return 0;
            double percent = (double)(value - low) / (high - low) * 100;
            percent = Math.Max(0, Math.Min(100, percent));
            return colorHigh ? percent : 100 - percent;
        }

        public static string GetYellowColorCode(int value, int low, int high, bool colorHigh)
        {
            double percent = GetPercent(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(255, 255, otherColor);
        }

        public static string GetRedColorCode(int value, int low, int high, bool colorHigh)
        {
            double percent = GetPercent(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(255, otherColor, otherColor);
        }

        public static string GetGreenColorCode(int value, int low, int high, bool colorHigh)
        {
            double percent = GetPercent(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent / 200 * 255, 0);
            return GetColorCode(otherColor, 255, otherColor);
        }

        public static string GetCyanColorCode(int value, int low, int high, bool colorHigh)
        {
            double percent = GetPercent(value, low, high, colorHigh);
            int otherColor = (int)Math.Round(255 - percent * 0.8, 0);
            return GetColorCode(otherColor, 255, 255);
        }
    }
}