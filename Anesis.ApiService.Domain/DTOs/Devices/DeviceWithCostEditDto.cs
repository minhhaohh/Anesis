using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.Devices
{
    public class DeviceWithCostEditDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VendorName { get; set; }

        public string DeviceCode { get; set; }

        public DeviceCategory? Category { get; set; }

        public bool IsActive { get; set; }

        public int? DisplayOrder { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public string Notes { get; set; }

        public void ApplyCostChanges(DeviceCost deviceCost)
        {
            deviceCost.VendorCost = VendorCost;
            deviceCost.AppliedCost = AppliedCost;
            deviceCost.EffectiveDate = EffectiveDate.Value;
            deviceCost.EndDate = EndDate.Value;
            deviceCost.UpdatedBy = "haotm";
            deviceCost.UpdatedDate = DateTime.Now;
            deviceCost.Notes = Notes;
        }

        public DeviceCost ToDeviceCost(DeviceAndSupply device)
        {
            return new DeviceCost()
            {
                Device = device,
                VendorCost = VendorCost,
                AppliedCost = AppliedCost,
                EffectiveDate = EffectiveDate.Value,
                EndDate = EndDate.Value,
                UpdatedBy = "haotm",
                UpdatedDate = DateTime.Now,
                Notes = Notes,
            };
        }

        public DeviceCost ToDeviceCost(int deviceId)
        {
            return new DeviceCost()
            {
                DeviceId = deviceId,
                VendorCost = VendorCost,
                AppliedCost = AppliedCost,
                EffectiveDate = EffectiveDate.Value,
                EndDate = EndDate.Value,
                UpdatedBy = "haotm",
                UpdatedDate = DateTime.Now,
                Notes = Notes,
            };
        }
    }
}
