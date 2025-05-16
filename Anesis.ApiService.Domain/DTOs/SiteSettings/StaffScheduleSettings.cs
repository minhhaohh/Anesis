namespace Anesis.ApiService.Domain.DTOs.SiteSettings
{
    public class StaffScheduleSettings : ISettings
    {
        /// <summary>
        /// Defines the label for time off resource on the calendar
        /// </summary>
        public string TimeOffResourceLabel { get; set; } = "Time Off";

        /// <summary>
        /// Defines the label for holiday resource on the calendar
        /// </summary>
        public string HolidayResourceLabel { get; set; } = "Holiday";

        /// <summary>
        /// Defines the time slot width in pixels on the calendar. This setting only takes effect in Week view
        /// </summary>
        public int SlotWidth { get; set; } = 48;

        /// <summary>
        /// Defines the frequency for displaying time slots on the calendar. This setting only takes effect in Week view
        /// </summary>
        public string SlotDuration { get; set; } = "01:00";

        /// <summary>
        /// Defines the end time of office working hours. This setting is only used for BelRed clinic.
        /// </summary>
        public string BelRedEndTime { get; set; } = "20:00";

        /// <summary>
        /// List of email addresses which will receive a notification email
        /// when employee's schedules are changed.
        /// </summary>
        public string ScheduleChangeInformEmails { get; set; }
    }
}
