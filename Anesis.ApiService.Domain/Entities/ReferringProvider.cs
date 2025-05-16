using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class ReferringProvider : IEntity
    {
        public int Id { get; set; }

        public string ProviderName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string FirstTelNumber { get; set; }

        public string SecondTelNumber { get; set; }

        public string CellPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string ClinicName { get; set; }

        public string SiteId { get; set; }

        public string NpiNumber { get; set; }

        public bool Verified { get; set; }

        public string Notes { get; set; }
    }
}
