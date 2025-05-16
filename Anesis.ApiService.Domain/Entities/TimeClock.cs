using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.Entities
{
    public class TimeClock : IEntity
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string ClockType { get; set; }

        public string SubType { get; set; }

        public DateTime ClockDate { get; set; }

        public DateTime? ClockIn { get; set; }

        public string ClockInIp { get; set; }

        public string ClockInLocation { get; set; }

        public DateTime? ClockOut { get; set; }

        public string ClockOutIp { get; set; }

        public string ClockOutLocation { get; set; }

        public string AccountId { get; set; }

        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        /// Approved, Pending, Exception
        /// </summary>
        public string Status { get; set; }

        public decimal TotalHours { get; set; }

        public decimal Deduction { get; set; }

        public string Paid { get; set; }

        public string Notes { get; set; }

        public int? ClosedPayrollId { get; set; }

        public int? EmployeeHistoryId { get; set; }

        public string ManagerNotes { get; set; }

        public TimeClockManual ManualClock { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string UserAgent { get; set; }

        public int? StartTime { get; set; }



        public Employee Employee { get; set; }

        public Account Approver { get; set; }

        public ClosedPayroll ClosedPayroll { get; set; }

        public EmployeeHistory EmployeeHistory { get; set; }
    }
}
