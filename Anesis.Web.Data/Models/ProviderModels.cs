using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class ProviderFilterModel : PageableSearchModelBase
    {
        public bool ActiveOnly { get; set; }
    }

    public class ProviderViewModel
    {
        public int Id { get; set; }

        public string ProviderType { get; set; }

        public string ProviderName { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsActive { get; set; }

        public string SiteId { get; set; }

        public string Notes { get; set; }

        public string NpiNumber { get; set; }

        public string DeaNumber { get; set; }

        public decimal? ProFeeBonusPct { get; set; }

        public int? EcwSiteId { get; set; }

        public string EcwCode { get; set; }
    }
}
