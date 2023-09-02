namespace ComputerStore.Classes
{
    public static class Extensions
    {
        public static bool HasValue(this string input)
        {
            return string.IsNullOrEmpty(input) == false && string.IsNullOrWhiteSpace(input) == false;
        }
    }
}
