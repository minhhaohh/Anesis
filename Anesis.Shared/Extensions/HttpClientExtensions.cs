using System.Collections;
using System.Web;

namespace Anesis.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        public static string ToQueryParams<T>(this T obj) where T : class
        {
            return obj
                .GetType()
                .GetProperties()
                .Where(x => x.GetValue(obj, null) != null && !IsEnumerableType(x.PropertyType))
                .Select(x => $"{HttpUtility.UrlEncode(x.Name)}={HttpUtility.UrlEncode(x.GetValue(obj)?.ToString())}")
                .StrJoin("&");
        }

        private static bool IsEnumerableType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }
    }
}
