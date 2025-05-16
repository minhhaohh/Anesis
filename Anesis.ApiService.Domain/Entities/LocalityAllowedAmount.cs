using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class LocalityAllowedAmount : IEntity
    {
        public int Id { get; set; }

        public string FeeType { get; set; }

        public int CountyId { get; set; }

        public int BillingCodeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal AllowedAmount { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }



        public GeneralCategory County { get; set; }

        public BillingCode BillingCode { get; set; }
    }
}
