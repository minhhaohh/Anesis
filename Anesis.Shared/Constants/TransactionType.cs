namespace Anesis.Shared.Constants
{
    public class TransactionType
    {
        public const string ALL = "all";
        public const string EFT = "eft";
        public const string DEPOSIT = "deposit";
        public const string CARD = "card";
        public const string CASH = "cash";
        public const string CHECK = "check";

        public const string REVENUE = "revenue";

        public static string[] GetPaymentTypes()
        {
            return [EFT, DEPOSIT, CARD];
        }
    }
}
