namespace Anesis.Shared.Constants
{
    public enum PotentialProcedureStatus
    {
        Pending = 0,
        Reviewed = 10,
        Ordered = 20,
        Scheduled = 40,
        Completed = 60,
        Cancelled = 100,
    }

    public static class PotentialProcedureReviewStatus
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Cancelled = "Cancelled";
    }
}
