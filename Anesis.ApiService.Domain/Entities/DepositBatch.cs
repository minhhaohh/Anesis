using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class DepositBatch : IEntity
    {
        public int Id { get; set; }

        public decimal? DepositAmount { get; set; }

        public DateTime? DepositDate { get; set; }

        public string TransactionId { get; set; }

        public string DepositedBy { get; set; }

        public bool ReconciledFlag { get; set; }

        public int? ReconciliationId { get; set; }

        public string Status { get; set; }

        public int? BankTransactionId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public bool MedicalRecords { get; set; }

        public string DepositTo { get; set; }

        public List<DepositBatchItem> DepositBatchItems { get; set; }

        public Reconciliation Reconciliation { get; set; }

        public BankTransaction BankTransaction { get; set; }
    }
}
