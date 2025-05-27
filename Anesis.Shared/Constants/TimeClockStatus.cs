namespace Anesis.Shared.Constants
{
    public static class TimeClockStatus
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Used = "Used";
        public const string Exception = "Exception";
        public const string Deleted = "Deleted";
        public const string Denied = "Denied";

        public static string[] All()
        {
            return [Pending, Approved, Used, Exception, Deleted, Denied];
        }
    }

    public static class TimeClockType
    {
        public const string Regular = "Regular";
        public const string PTO = "PTO";
        public const string CME = "CME";
        public const string Unpaid = "Unpaid";
        public const string Holiday = "Holiday";
        public const string OtherPaid = "OtherPaid";

        public static string[] GetRequiredApprovingTypes()
        {
            return [PTO, Unpaid, CME, OtherPaid];
        }

        public static string[] GetTimeOffHourTypes()
        {
            return [Unpaid, PTO, CME, OtherPaid];
        }
    }

    public enum TimeClockManual
    {
        None = 0,
        In = 1,
        Out = 2,
        InOut = In | Out,
        Auto = 4,
        InAuto = In | Auto
    }

    public static class TimeClockUpdateAction
    {
        public const string ClockIn = "ClockIn";
        public const string ClockOut = "ClockOut";
        public const string Request = "Request";
        public const string Approve = "Approve";
        public const string Unapproved = "Unapproved";
        public const string Deny = "Deny";
        public const string Holiday = "Holiday";
        public const string Edit = "Edit";
        public const string ChangeNotes = "Change Notes";
        public const string Delete = "Delete";
        public const string Close = "Close";
    }

    public static class PtoAccrualPeriod
    {
        public const string Biweekly = "BIWEEKLY";
        public const string Monthly = "MONTHLY";

        public static string[] All()
        {
            return [Biweekly, Monthly];
        }
    }
}
