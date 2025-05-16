namespace Anesis.Web.Data.Models
{
    public class ReconciliationViewModel
    {
        public int Id { get; set; }

        public DateTime ReconciliationDate { get; set; }

        public DateTime ReconciledThru { get; set; }

        public string ReconciledBy { get; set; }

        public decimal? DepositBalance { get; set; }

        public string DateThruAndBy { get; set; }
    }
}
