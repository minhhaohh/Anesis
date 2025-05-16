using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class BillingCode : IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string EncounterType { get; set; }

        public string ClinicalSynopsis { get; set; }

        public bool PreAuthRequired { get; set; }

        public int? EcwSiteId { get; set; }
    }
}
