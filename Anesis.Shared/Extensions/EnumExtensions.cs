using Anesis.Shared.Models;

namespace Anesis.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static List<IntStringPair> ToIntStringPair(this Type type)
        {
            return Enum.GetValues(type)
                .Cast<int>()
                .Select(x => new IntStringPair()
                {
                    Id = x,
                    Value = Enum.GetName(type, x)
                })
                .ToList();
        }

        public static Dictionary<int, string> ToDictionary(this Type type)
        {
            return Enum.GetValues(type)
                .Cast<int>()
                .ToDictionary(x => x, x => Enum.GetName (type, x) );
        }
    }
}
