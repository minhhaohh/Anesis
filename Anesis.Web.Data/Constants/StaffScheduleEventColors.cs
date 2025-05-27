namespace Anesis.Web.Data.Constants
{
    public static class StaffScheduleEventColors
    {
        public const string ClinicClosed = "event-clinic-closed";

        public const string TimeOff = "event-time-off";

        public const string Holiday = "event-holiday";

        public const string Manager = "event-manager";

        public const string Doctor = "event-doctor";

        public const string Employee = "event-employee";

        public const string OtherEmployee = "event-other-employee";


        public const string DefaultEventType = nameof(Employee);

        public static Dictionary<string, string> All()
        {
            return new Dictionary<string, string>()
            {
                [nameof(ClinicClosed)] = ClinicClosed,
                [nameof(TimeOff)] = TimeOff,
                [nameof(Holiday)] = Holiday,
                [nameof(Manager)] = Manager,
                [nameof(Doctor)] = Doctor,
                [nameof(Employee)] = Employee,
                [nameof(OtherEmployee)] = OtherEmployee,
            };
        }


        public static string GetColorClass(string eventType, string defaultEventType)
        {
            return All().GetValueOrDefault(eventType, All().GetValueOrDefault(defaultEventType, ""));
        }
    }
}
