namespace Anesis.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static string StrJoin(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static string StrJoin(this IEnumerable<int> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}
