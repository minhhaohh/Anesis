using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class EmployeeHistory : IEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string UpdatedAction { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime HireDate { get; set; }

        public string Title { get; set; }

        //public string EmpRole { get; set; }

        public string EmpType { get; set; }

        public string Exempt { get; set; }

        public string Company { get; set; }

        public string Status { get; set; }

        public decimal LunchAllowance { get; set; }

        public string TermType { get; set; }

        public DateTime? TermDate { get; set; }

        public string TermReason { get; set; }

        public int? ManagerId { get; set; }

        public int? LocationId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Notes { get; set; }

        public string ChangedFields { get; set; }

        public string HistoryStatus { get; set; }

        public string PtoNotificationEmail { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactTel { get; set; }

        public string EmergencyContactRelationship { get; set; }

        public string PersonalEmail { get; set; }

        public string PersonalPhone { get; set; }

        public decimal HoursPerDay { get; set; }

        public decimal DaysPerWeek { get; set; }

        public int? JobRoleId { get; set; }

        public bool IncludedInPayroll { get; set; }



        public Employee Employee { get; set; }

        public Employee Manager { get; set; }

        public Account Account { get; set; }

        public Location Location { get; set; }

        public JobRole JobRole { get; set; }
    }
}
