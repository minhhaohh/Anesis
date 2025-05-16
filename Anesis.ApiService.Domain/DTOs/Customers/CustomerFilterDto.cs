using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.Customers
{
    public class CustomerFilterDto : PageableSearchModelBase
    {
        public string CustomerName { get; set; }

        public CustomerType? CustomerTypeId { get; set; }

        public bool ActiveOnly { get; set; }
    }
}
