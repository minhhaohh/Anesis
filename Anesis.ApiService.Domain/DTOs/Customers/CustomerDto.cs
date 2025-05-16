using Anesis.ApiService.Domain.Constants;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCode { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string EmailAddress { get; set; }

        public string Website { get; set; }

        public bool IsActive { get; set; }

        public CustomerType CustomerTypeId { get; set; }

        public string CustomerTypeStr => CustomerTypeId.ToString();

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }
    }
}
