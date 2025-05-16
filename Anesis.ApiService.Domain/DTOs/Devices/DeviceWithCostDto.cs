using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.Devices
{
    public class DeviceWithCostDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VendorName { get; set; }

        public string DeviceCode { get; set; }

        public DeviceCategory Category { get; set; }

        public string CategoryName => Category.ToString();

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public bool CanEditDevice => true;
    }
}
