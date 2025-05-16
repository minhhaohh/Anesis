using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Anesis.ApiService.EntityFrameworkCore.Conversions
{
    public class NullableTimeSpanToIntConverter : ValueConverter<TimeSpan?, int?>
    {
        public NullableTimeSpanToIntConverter() : base(
            v => v.HasValue ? v.Value.Hours * 60 + v.Value.Minutes : null,
            v => v.HasValue ? new TimeSpan(v.Value / 60, v.Value % 60, 0) : null)
        {
        }
    }
}
