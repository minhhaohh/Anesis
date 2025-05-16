using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Provider : IEntity, IActivatable
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

        public bool CredentialingVisible { get; set; }

        public int CredentialingDisplayOrder { get; set; }

        public bool CredentialingRequired { get; set; }
    }
}
