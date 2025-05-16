using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class DepositFilterModel : PageableSearchModelBase
    {
        public DateTime? CreatedFromDate { get; set; }

        public string Status { get; set; }
    }

    public class DepositViewModel
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


        public List<DepositItemViewModel> DepositItems { get; set; }
    }

    public class DepositItemViewModel
    {
        public int DepositBatchId { get; set; }

        public int ItemNumber { get; set; }

        public string DepositType { get; set; }

        public string ReceivedFrom { get; set; }

        public DateTime ReceivedDate { get; set; }

        public decimal ItemAmount { get; set; }

        public string ReceivedBy { get; set; }

        public string Status { get; set; }

        public bool IsAutoLinked { get; set; }
    }
}
