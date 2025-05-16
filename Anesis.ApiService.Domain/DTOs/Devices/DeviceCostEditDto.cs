using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.Devices
{
    public class DeviceCostEditDto
    {
        public int DeviceId { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public string Notes { get; set; }

        public void ApplyChangesTo(DeviceCost deviceCost)
        {
            deviceCost.VendorCost = VendorCost;
            deviceCost.AppliedCost = AppliedCost;
            deviceCost.EffectiveDate = EffectiveDate.Value;
            deviceCost.UpdatedDate = DateTime.Now;
            deviceCost.UpdatedBy = "haotm";
            deviceCost.Notes = Notes;
        }

        public DeviceCost ToDeviceCost()
        {
            return new DeviceCost()
            {
                DeviceId = DeviceId,
                VendorCost = VendorCost,
                AppliedCost = AppliedCost,
                EffectiveDate = EffectiveDate.Value,
                UpdatedDate = DateTime.Now,
                UpdatedBy = "haotm",
                Notes = Notes,
            };
        }
    }
}
