namespace Anesis.Web.Data.Constants
{
    public static class InvoiceStatusColors
    {
        public const string Pending = "rz-color-primary";
        public const string Dispute = "rz-color-warning";
        public const string Approved = "rz-color-info";
        public const string Paid = "rz-color-success";
        public const string Voided = "rz-color-danger";

        public static Dictionary<string, string> All()
        {
            return new Dictionary<string, string>()
            {
                [nameof(Pending)] = Pending,
                [nameof(Dispute)] = Dispute,
                [nameof(Approved)] = Approved,
                [nameof(Paid)] = Paid,
                [nameof(Voided)] = Voided,
            };
        }

        public static string GetColorClass(string status)
        {
            return All().GetValueOrDefault(status, "");
        }
    }
}
