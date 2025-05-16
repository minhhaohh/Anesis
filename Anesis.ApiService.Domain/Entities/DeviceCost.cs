using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class DeviceCost : IEntity
	{
		public int Id { get; set; }

		public int DeviceId { get; set; }

		public DateTime EffectiveDate { get; set; }

		public DateTime? EndDate { get; set; }

		public decimal VendorCost { get; set; }

		public decimal AppliedCost { get; set; }

		public DateTime UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public string Notes { get; set; }


		public DeviceAndSupply Device { get; set; }
	}
}
