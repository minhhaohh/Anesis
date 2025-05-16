using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class SurgeryCaseCptCode : IEntity
    {
        public int Id { get; set; }

        public int SurgeryCaseId { get; set; }

        public int BillingCodeId { get; set; }

        public int RvuConfigId { get; set; }

        public int Quantity { get; set; }

        public int AllowedAmountId { get; set; }

        public decimal AllowedAmountPct { get; set; }

        public decimal? CollectedAmount { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }



        public SurgeryCase SurgeryCase { get; set; }

        public BillingCode BillingCode { get; set; }

        public RvuConfig RvuConfig { get; set; }

        public LocalityAllowedAmount AllowedAmount { get; set; }
    }
}
