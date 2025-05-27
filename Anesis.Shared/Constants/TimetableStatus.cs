namespace Anesis.Shared.Constants
{
    public enum ScheduleEventType
    {
        Unknown = 0,
        Other = 1,
        Terminated = 2,
        Weekend = 10,
        Holiday = 20,
        ClinicClosed = 30,
        Meeting = 40,
        ReservedResource = 50,
        TimeOff = 60,
        Provider = 80,
        SupportStaff = 90,
        Anesthesiologist = 100,
        StaffSchedule = 200,
    }

    public static class ScheduleResourceType
    {
        public static string Location = "location";

        public static string Employee = "employee";

        public static string[] All()
        {
            return [ Location, Employee ];
        }
    }

    public static class StaffScheduleEventType
    {
        public const string ClinicClosed = "ClinicClosed";

        public const string TimeOff = "TimeOff";

        public const string Holiday = "Holiday";

        public const string Manager = "Manager";

        public const string Doctor = "Doctor";

        public const string Employee = "Employee";

        public const string OtherEmployee = "OtherEmployee";
    }

    public static class StaffScheduleSaveOption
    {
        public const string OverwriteExisting = "Overwrite existing schedules";

        public const string IgnoreConflicts = "Ignore conflicts and try to schedule on available days";

        public const string ShowErrors = "Show errors if there are any conflicts";

        public static string[] All()
        {
            return [
                OverwriteExisting,
                IgnoreConflicts,
                ShowErrors
            ];
        }
    }
}
