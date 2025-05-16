using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Anesis.ApiService.EntityFrameworkCore.Conversions
{
    public class TimeSpanToIntConverter : ValueConverter<TimeSpan, int>
    {
        public TimeSpanToIntConverter()
            : base(v => v.Hours * 60 + v.Minutes, v => new TimeSpan(v / 60, v % 60, 0))
        {
        }
    }
}
