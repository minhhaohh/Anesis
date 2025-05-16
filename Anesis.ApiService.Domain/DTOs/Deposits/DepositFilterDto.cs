using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.Deposits
{
    public class DepositFilterDto : PageableSearchModelBase
    {
        public DateTime? CreatedFromDate { get; set; }

        public string Status { get; set; }
    }
}
