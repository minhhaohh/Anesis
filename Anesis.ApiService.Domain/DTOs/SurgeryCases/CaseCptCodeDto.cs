namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCptCodeDto
    {
        public int Id { get; set; }

        public int SurgeryCaseId { get; set; }

        public int BillingCodeId { get; set; }

        public string CptCode { get; set; }

        public int RvuConfigId { get; set; }

        public DateTime RvuEffectiveDate { get; set; }
        
        public decimal WorkRvu { get; set; }

        public decimal ConversionFactor { get; set; }

        public int Quantity { get; set; }

        public int AllowedAmountId { get; set; }

        public decimal CmsAllowedAmt { get; set; }

        public decimal AllowedAmountPct { get; set; }

        public decimal? CollectedAmount { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public decimal ContractedPct
            => AllowedAmountPct % 100;

        public decimal AllowedAmtValue
            => CmsAllowedAmt * AllowedAmountPct * Quantity;
    }
}
