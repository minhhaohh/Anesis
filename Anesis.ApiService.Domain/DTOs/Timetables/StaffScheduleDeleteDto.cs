using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class StaffScheduleDeleteDto
    {
        public int? Id { get; set; }

        public int? LocationId { get; set; }

        public string EmployeeIdList { get; set; }

        public List<int> EmployeeIds => EmployeeIdList.HasValue()
            ? EmployeeIdList.Split(",").Select(int.Parse).ToList()
            : new List<int>();

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string DaysOfWeekList { get; set; }

        public List<int> DaysOfWeek => DaysOfWeekList.HasValue()
            ? DaysOfWeekList.Split(",").Select(int.Parse).ToList()
            : new List<int>();

        public bool IsDeleteById()
        {
            return Id > 0 && EmployeeIds.Count <= 1 && ToDate == null;
        }
    }
}
