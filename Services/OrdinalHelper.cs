namespace RotoMonsterUI
{
    public static class OrdinalHelper
    {
        public static string GetOrdinal(int number)
        {
            if (number <= 0) return "1st";
            
            string suffix;
            int lastTwo = number % 100;
            int lastOne = number % 10;

            if (lastTwo >= 11 && lastTwo <= 13)
                suffix = "th";
            else if (lastOne == 1)
                suffix = "st";
            else if (lastOne == 2)
                suffix = "nd";
            else if (lastOne == 3)
                suffix = "rd";
            else
                suffix = "th";

            return $"{number}{suffix}";
        }
    }
}