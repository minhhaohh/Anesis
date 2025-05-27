using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class StaffScheduleFilterDto
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ResourceType { get; set; }

        public List<int> ResourceIds => ResourceIdList.HasValue()
            ? ResourceIdList.Split(",").Select(int.Parse).ToList()
            : new List<int>();

        public string ResourceIdList { get; set; }

        public List<int> HiddenDaysOfWeek
            => IsHiddenWeekends ? new List<int> { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday } : new List<int>();

        public bool IsHiddenWeekends { get; set; }
    }
}
