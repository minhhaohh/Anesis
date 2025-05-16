using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class BankTransaction : IEntity
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string Reference { get; set; }

        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public string TransactionType { get; set; }

        public decimal? Debit { get; set; }

        public decimal? Credit { get; set; }

        public bool ReconciledFlag { get; set; }

        public int? ReconciliationId { get; set; }

        public string HashKey { get; set; }

        public virtual Reconciliation Reconciliation { get; set; }
    }
}
