using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCostFilterDto : PageableSearchModelBase
    {
        public int CaseId { get; set; }
    }
}
