namespace Anesis.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<int, string> Clone(this Dictionary<int, string> source)
        {
            var result = new Dictionary<int, string>();
            foreach (var item in source)
            {
                result.Add(item.Key, item.Value);
            }
            return result;
        }

        public static void AddMany(this Dictionary<int, string> source, Dictionary<int, string> dicts)
        {
            foreach (var item in dicts)
            {
                source.Add(item.Key, item.Value);
            }
        }
    }
}
