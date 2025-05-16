using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime HireDate { get; set; }

        public string Title { get; set; }

        public string EmpType { get; set; }

        public string Exempt { get; set; }

        public string Company { get; set; }

        public string Status { get; set; }

        public decimal LunchAllowance { get; set; }

        public string TermType { get; set; }

        public DateTime? TermDate { get; set; }

        public string TermReason { get; set; }

        public int? ManagerId { get; set; }

        public string ManagerName { get; set; }

        public int? LocationId { get; set; }

        public string LocationName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Notes { get; set; }

        public string PtoNotificationEmail { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactTel { get; set; }

        public string EmergencyContactRelationship { get; set; }

        public string PersonalEmail { get; set; }

        public string PersonalPhone { get; set; }

        public decimal HoursPerDay { get; set; }

        public decimal DaysPerWeek { get; set; }

        public int? JobRoleId { get; set; }

        public string JobRoleName { get; set; }

        public bool IncludedInPayroll { get; set; }

        public string FullAddress => Address.IsNullOrWhiteSpace() ? "" : $"{Address}, {City}, {State} {ZipCode}";

        public string FullName => $"{FirstName} {LastName}";
    }
}
