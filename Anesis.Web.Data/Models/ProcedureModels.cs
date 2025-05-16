using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class ProcedureFilterModel : PageableSearchModelBase
    {
        public string Name { get; set; }

        public bool HasSiteId { get; set; }

        public bool ActiveOnly { get; set; }
    }

    public class ProcedureViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string SiteId { get; set; }

        public string Checklist { get; set; }

        public bool PreAuthRequired { get; set; }

        public int Type { get; set; }

        public int TypeName { get; set; }

        public int? CategoryId { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }

        public int? EcwSiteId { get; set; }

        public decimal BonusPoint { get; set; }
    }
}
