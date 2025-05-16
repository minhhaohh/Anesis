using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime HireDate { get; set; }

        public string Title { get; set; }

        ///// <summary>
        ///// MA, Midlevel, Doctor, Executive, Billing
        ///// </summary>
        //public string EmpRole { get; set; }

        /// <summary>
        /// full, part
        /// </summary>
        public string EmpType { get; set; }

        /// <summary>
        /// salary, hourly, contractor, temp
        /// </summary>
        public string Exempt { get; set; }

        /// <summary>
        /// Anesis, Pain Care
        /// </summary>
        public string Company { get; set; }

        public string Status { get; set; }

        public decimal LunchAllowance { get; set; }

        /// <summary>
        /// voluntary, involuntary, mutual
        /// </summary>
        public string TermType { get; set; }

        public DateTime? TermDate { get; set; }

        public string TermReason { get; set; }

        public int? ManagerId { get; set; }

        /// <summary>
        /// Main workplace id
        /// </summary>
        public int? LocationId { get; set; }

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

        public bool IncludedInPayroll { get; set; }


        /// <summary>
        /// Direct manager
        /// </summary>
        public Employee Manager { get; set; }

        /// <summary>
        /// Main workplace
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Job role or job position
        /// </summary>
        public JobRole JobRole { get; set; }

        public string FullAddress => Address.IsNullOrWhiteSpace() ? "" : $"{Address}, {City}, {State} {ZipCode}";

        public string FullName => $"{FirstName} {LastName}";
    }
}
