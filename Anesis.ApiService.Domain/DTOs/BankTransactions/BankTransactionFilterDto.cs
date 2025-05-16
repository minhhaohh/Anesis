using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.BankTransactions
{
    public class BankTransactionFilterDto : PageableSearchModelBase
    {
        public string BatchType { get; set; }

        public DateTime? ReconciledDate { get; set; }

        public string BankType { get; set; }

        public int? ReconciliationId { get; set; }
    }
}
