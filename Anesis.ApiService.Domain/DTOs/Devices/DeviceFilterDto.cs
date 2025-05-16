using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.Devices
{
    public class DeviceFilterDto : PageableSearchModelBase
    {
        public string Name { get; set; }

        public string VendorName { get; set; }

        public int? Category { get; set; }

        public bool ActiveOnly { get; set; }
    }
}
