using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.Locations
{
    public class LocationFilterDto : PageableSearchModelBase
    {
        public string LocationType { get; set; }

        public bool HasSiteId { get; set; }

        public bool ActiveOnly { get; set; }
    }
}
