namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class StaffScheduleDeleteDto
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public List<int> EmployeeIds { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<int> DaysOfWeek { get; set; }

        public bool IsSingleEvent()
        {
            return Id > 0 && EmployeeIds.Count == 1 && ToDate == null;
        }
    }
}
