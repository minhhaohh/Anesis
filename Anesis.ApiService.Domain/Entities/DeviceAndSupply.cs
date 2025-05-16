using Anesis.Shared.Constants;
using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class DeviceAndSupply : IEntity, IActivatable, IDisplayOrder
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string VendorName { get; set; }

		public string DeviceCode { get; set; }

		public DeviceCategory Category { get; set; }

		public bool IsActive { get; set; }

		public int DisplayOrder { get; set; }

		public DateTime CreatedDate { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public string Notes { get; set; }
	}
}
