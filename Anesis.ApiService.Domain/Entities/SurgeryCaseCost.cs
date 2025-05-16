using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class SurgeryCaseCost : IEntity
    {
        public int Id { get; set; }

        public int SurgeryCaseId { get; set; }

        public int DeviceId { get; set; }

        public int CostId { get; set; }

        public int Quantity { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }


        public SurgeryCase SurgeryCase { get; set; }

        public DeviceAndSupply Device { get; set; }

        public DeviceCost DeviceCost { get; set; }
    }
}
