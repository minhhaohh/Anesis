using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class RvuConfig : IEntity
    {
        public int Id { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int BillingCodeId { get; set; }

        public decimal WorkRvu { get; set; }

        public decimal MalpracticeRvu { get; set; }

        public decimal NonFacilityPeRvu { get; set; }

        public decimal NonFacilityRvuTotal { get; set; }

        public decimal FacilityPeRvu { get; set; }

        public decimal FacilityRvuTotal { get; set; }

        public decimal ConversionFactor { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }


        public BillingCode BillingCode { get; set; }
    }
}
