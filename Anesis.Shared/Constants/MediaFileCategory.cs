namespace Anesis.Shared.Constants
{
    public class MediaFileCategory
    {
        public const string BillingInvoice = "Billing Invoice";

        public const string SurgicalDoc = "Surgical Document";

        public const string UserGuide = "User Guide";

        public const string Announcement = "Announcement";

        public static string[] GetAll()
        {
            return [BillingInvoice, SurgicalDoc, UserGuide, Announcement];
        }
    }
}
