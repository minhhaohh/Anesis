using Microsoft.AspNetCore.Identity;

namespace Anesis.ApiService.Domain.Entities
{
    public class Account : IdentityUser<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? PwdChangedDate { get; set; }

        public int? EcwSiteId { get; set; }


        public Employee Employee { get; set; }


        public string FullName => FirstName + " " + LastName;
    }
}
