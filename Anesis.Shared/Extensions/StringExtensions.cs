namespace Anesis.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string IfNullOrWhiteSpace(this string str, string defaultValue)
        {
            return str.IsNullOrWhiteSpace() ? defaultValue : str;
        }

        public static string IfNullOrEmpty(this string str, string defaultValue)
        {
            return str.IsNullOrEmpty() ? defaultValue : str;
        }

        public static string UpperFirstLetter(this string str)
        {
            return str.IsNullOrEmpty() ? str : char.ToUpper(str[0]) + str.Substring(1);
        }

        public static int ToInt(this string value, int defaultValue = 0)
        {
            return int.Parse(value);
        }

        public static string EnsureStartsWith(this string value, string startsWith)
        {
            if (!value.StartsWith(startsWith))
            {
                return startsWith + value;
            }

            return value;
        }

        public static string EnsureEndsWith(this string value, string endsWith)
        {
            if (!value.EndsWith(endsWith))
            {
                return value + endsWith;
            }

            return value;
        }
    }
}
