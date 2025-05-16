using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Insurance : IEntity
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string InsuranceCategory { get; set; }

        public bool PreAuthKnown { get; set; }

        public int? ParentId { get; set; }

        public int? PreAuthInsGroupId { get; set; }

        public int? EcwSiteId { get; set; }

        public string Notes { get; set; }

        public bool CredentialingVisible { get; set; }

        public int CredentialingDisplayOrder { get; set; }

        public bool CredentialingRequired { get; set; }

        public PreAuthInsuranceGroup PreauthInsuranceGroup { get; set; }
    }
}
