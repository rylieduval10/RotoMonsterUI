namespace RotoMonsterUI
{
    public static class SingularPlural
    {
        public static string Get(string word, int count)
        {
            return count == 1 ? word : word + "s";
        }
    }
}