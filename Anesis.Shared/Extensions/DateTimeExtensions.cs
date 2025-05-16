namespace Anesis.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTimeString(this TimeSpan? time, bool use24HoursFormat = false)
        {
            return time.HasValue ? ToTimeString(time.Value, use24HoursFormat) : null;
        }

        public static string ToTimeString(this TimeSpan time, bool use24HoursFormat = false)
        {
            var hours = time.Hours;
            var minutes = time.Minutes;

            if (use24HoursFormat) return $"{hours:00}:{minutes:00}";

            var session = "AM";

            if (hours >= 12)
            {
                session = "PM";
                hours = hours > 12 ? hours - 12 : hours;
            }

            return $"{hours:00}:{minutes:00} {session}";
        }

        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            DateTimeOffset utcDateTime = dateTime.ToUniversalTime();
            return utcDateTime.ToUnixTimeSeconds();
        }

        public static DateTime ToLastDateOfNextMonth(this DateTime value)
        {
            // Get the first day of the next month
            var firstDayOfNextMonth = new DateTime(value.Year, value.Month, 1).AddMonths(1);

            // Get the last day of the next month
            return firstDayOfNextMonth.AddMonths(1).AddDays(-1);
        }

    }
}
