using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class DeviceFilterModel : PageableSearchModelBase
    {
        public int? Category { get; set; }

        public string Name { get; set; }

        public string VendorName { get; set; }

        public bool ActiveOnly { get; set; }
    }

    public class DeviceWithCostViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VendorName { get; set; }

        public string DeviceCode { get; set; }

        public int Category { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public DeviceWithCostEditModel ToDeviceWithCostEditModel()
        {
            return new DeviceWithCostEditModel()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                VendorName = VendorName,
                DeviceCode = DeviceCode,
                Category = Category,
                IsActive = IsActive,
                DisplayOrder = DisplayOrder,
                VendorCost = VendorCost,
                AppliedCost = AppliedCost,
                EffectiveDate = EffectiveDate,
                EndDate = EndDate,
                Notes = Notes,
            };
        }
    }

    public class DeviceWithCostEditModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VendorName { get; set; }

        public string DeviceCode { get; set; }

        public int? Category { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public string Notes { get; set; }
    }

    public class DeviceCostViewModel
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public DeviceCostEditModel ToDeviceCostEditModel()
        {
            return new DeviceCostEditModel()
            {
                Id = Id,
                DeviceId = DeviceId,
                VendorCost = VendorCost,
                AppliedCost = AppliedCost,
                EffectiveDate = EffectiveDate,
                EndDate = EndDate,
                Notes = Notes,
            };
        }
    }

    public class DeviceCostEditModel
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool OverwriteEffDate { get; set; }

        public string Notes { get; set; }
    }
}
