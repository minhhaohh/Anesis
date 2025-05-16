using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Patient : IEntity
    {
        public int Id { get; set; }

        public string ChartNo { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Gender { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

        public string SiteId { get; set; }

        public int RiskLevelId { get; set; }

        public string EcwChartNo { get; set; }

        public int? EcwSiteId { get; set; }


        public RiskLevel RiskLevel { get; set; }
    }
}
