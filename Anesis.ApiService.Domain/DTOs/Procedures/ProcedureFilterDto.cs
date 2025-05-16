using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.Procedures
{
    public class ProcedureFilterDto : PageableSearchModelBase
    {
        public string Name { get; set; }

        public bool HasSiteId { get; set; }

        public bool ActiveOnly { get; set; }
    }
}
