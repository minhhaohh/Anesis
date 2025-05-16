namespace Anesis.Web.Data.Models
{
    public class CreditTransactionViewModel
    {
        public int Id { get; set; }

        public string LocationId { get; set; }

        public string DbaName { get; set; }

        public string CurrencyCode { get; set; }

        public string TerminalId { get; set; }

        public string BatchNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime SubmitDate { get; set; }

        public string CardType { get; set; }

        public string CardHolderNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string AuthCode { get; set; }

        public string IcCode { get; set; }

        public DateTime UpdatedTime { get; set; }

        public int? BankTransactionId { get; set; }

        public int? ReconciliationId { get; set; }

        public string ChartNo { get; set; }

        public string PatientName { get; set; }

        public string PaymentType { get; set; }
    }
}
