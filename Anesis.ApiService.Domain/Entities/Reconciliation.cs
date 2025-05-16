using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Reconciliation : IEntity
    {
        public int Id { get; set; }

        public DateTime ReconciliationDate { get; set; }

        public DateTime? ReconciledThru { get; set; }

        public string ReconciledBy { get; set; }

        public decimal? DepositBalance { get; set; }
    }
}
