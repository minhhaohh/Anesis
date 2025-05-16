using Anesis.ApiService.Domain.Constants;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.Procedures
{
    public class ProcedureDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string SiteId { get; set; }

        public string Checklist { get; set; }

        public bool PreAuthRequired { get; set; }

        public ProcedureType Type { get; set; }

        public string TypeName => Type.ToString();

        public int? CategoryId { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Status { get; set; }

        public string Notes { get; set; }

        public int? EcwSiteId { get; set; }

        public decimal BonusPoint { get; set; }

    }
}
