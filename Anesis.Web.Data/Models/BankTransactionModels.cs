using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class BankTransactionFilterModel : PageableSearchModelBase
    {
        public string BatchType { get; set; }

        public DateTime? ReconciledDate { get; set; }

        public string BankType { get; set; }

        public int? ReconciliationId { get; set; }

        public BankTransactionFilterModel() 
        {
        }
    }

    public class BankTransactionViewModel
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string Reference { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public string TransactionType { get; set; }

        public decimal Balance { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public bool ReconciledFlag { get; set; }

        public int? ReconciliationId { get; set; }

        public string HashKey { get; set; }

        public bool HasDepositDetails { get; set; }

        public bool HasCreditDetails { get; set; }

        public bool CanSplit { get; set; }
    }

    public class BalanceSummaryViewModel
    {
        public decimal BankTotal { get; set; }

        public decimal MatchedTotal { get; set; }

        public decimal Different { get; set; }
    }

    public class BankAdjustmentCreateModel
    {
        public DateTime? PostDate { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }
    }

    public class BankTransactionSplitModel
    {
        public int Id { get; set; }

        public decimal SplitAmount { get; set; }

        public string Notes { get; set; }
    }
}
