namespace Anesis.ApiService.Domain.DTOs.BatchTransactions
{
    public class BatchTransactionDto
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string BatchOwner { get; set; }

        public string BatchId { get; set; }

        public string PaymentFrom { get; set; }

        public string PaymentType { get; set; }

        public decimal PaymentAmount { get; set; }

        public bool ReconciledFlag { get; set; }

        public int? ReconciliationId { get; set; }

        public string Description { get; set; }

        public int? LocationId { get; set; }

        public int? BankTransactionId { get; set; }

        public string TransactionType { get; set; }

        public string DocNo { get; set; }

        public string SourceSite { get; set; }
    }
}
