using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.BatchTransactions
{
    public class BatchTransactionFilterDto : PageableSearchModelBase
    {
        public string BatchType { get; set; }

        public DateTime? FromDate { get; set; }

        public string SearchText { get; set; }

        public int? ReconciliationId { get; set; }
    }
}
