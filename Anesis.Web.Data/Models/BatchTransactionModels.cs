using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class BatchTransactionFilterModel : PageableSearchModelBase
    {
        public string BatchType { get; set; }

        public DateTime? FromDate { get; set; }

        public string SearchText { get; set; }

        public int? ReconciliationId { get; set; }

        public BatchTransactionFilterModel()
        {
        }
    }

    public class BatchTransactionViewModel
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

    public class BatchLinkBankIdModel
    {
        public int BatchTransactionId { get; set; }

        public int BankTransactionId { get; set; }
    }

    public class BatchAdjustmentCreateModel
    {
        public DateTime? PostDate { get; set; }

        public string BatchOwner { get; set; }

        public int? LocationId { get; set; }

        public string Description { get; set; }

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }
    }

    public class BatchTransactionSplitModel
    {
        public int Id { get; set; }

        public decimal SplitAmount { get; set; }
    }
}
