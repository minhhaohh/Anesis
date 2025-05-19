namespace Anesis.Web.Data.Constants
{
    public static class ProposalStatusColors
    {
        public const string Pending = "rz-color-warning";
        public const string Reviewed = "rz-color-info";
        public const string Ordered = "rz-color-secondary";
        public const string Scheduled = "rz-color-primary";
        public const string Completed = "rz-color-success";
        public const string Cancelled = "rz-color-danger";

        public static Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>()
            {
                [nameof(Pending)] = Pending,
                [nameof(Reviewed)] = Reviewed,
                [nameof(Ordered)] = Ordered,
                [nameof(Scheduled)] = Scheduled,
                [nameof(Completed)] = Completed,
                [nameof(Cancelled)] = Cancelled,
            };
        }

        public static string GetColorClass(string status)
        {
            return GetAll().GetValueOrDefault(status, "");
        }
    }

    public static class ProposalReviewStatusColors
    {
        public const string Pending = "rz-color-warning";
        public const string Approved = "rz-color-success";
        public const string Cancelled = "rz-color-danger";

        public static Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>()
            {
                [nameof(Pending)] = Pending,
                [nameof(Approved)] = Approved,
                [nameof(Cancelled)] = Cancelled,
            };
        }

        public static string GetColorClass(string status)
        {
            return GetAll().GetValueOrDefault(status, "");
        }
    }
}
