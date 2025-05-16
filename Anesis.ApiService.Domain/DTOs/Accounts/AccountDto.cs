namespace Anesis.ApiService.Domain.DTOs.Accounts
{
    public class AccountDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }
        
        public bool TwoFactorEnabled { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? PwdChangedDate { get; set; }

        public int? EcwSiteId { get; set; }
    }
}
