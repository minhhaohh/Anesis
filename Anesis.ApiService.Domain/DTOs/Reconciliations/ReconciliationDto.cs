namespace Anesis.ApiService.Domain.DTOs.Reconciliations
{
    public class ReconciliationDto
    {
        public int Id { get; set; }

        public DateTime ReconciliationDate { get; set; }

        public DateTime? ReconciledThru { get; set; }

        public string ReconciledBy { get; set; }

        public decimal? DepositBalance { get; set; }

        public string DateThruAndBy => $"{ReconciledThru:MM/dd/yyyy} (by {ReconciledBy})";
    }
}
