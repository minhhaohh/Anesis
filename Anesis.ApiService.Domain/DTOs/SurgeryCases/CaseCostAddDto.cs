using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCostAddDto
    {
        public int? CaseId { get; set; }

        public int? DeviceId { get; set; }

        public int? Quantity { get; set; }

        public string Notes { get; set; }

        public SurgeryCaseCost ToSurgeryCaseCost(DeviceCostDto currentCost)
        {
            return new SurgeryCaseCost()
            {
                SurgeryCaseId = CaseId.Value,
                DeviceId = DeviceId.Value,
                Quantity = Quantity.Value,
                CostId = currentCost.Id,
                AppliedCost = currentCost.AppliedCost,
                UpdatedBy = "haotm",
                UpdatedDate = DateTime.Now,
                Notes = Notes
            };
        }
    }
}
