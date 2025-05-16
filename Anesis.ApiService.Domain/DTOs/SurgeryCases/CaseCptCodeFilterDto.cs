using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCptCodeFilterDto : PageableSearchModelBase
    {
        public int CaseId { get; set; }
    }
}
