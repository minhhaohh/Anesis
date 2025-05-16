using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.Providers
{
    public class ProviderFilterDto : PageableSearchModelBase
    {
        public bool ActiveOnly { get; set; }
    }
}
