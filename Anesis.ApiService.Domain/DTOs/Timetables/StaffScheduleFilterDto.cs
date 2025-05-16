using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class StaffScheduleFilterDto
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ResourceType { get; set; }

        public List<int> ResourceIdList => ResourceIds.HasValue()
            ? ResourceIds.Split(",").Select(int.Parse).ToList()
            : new List<int>();

        public string ResourceIds { get; set; }

        public List<int> HiddenDaysOfWeek
            => IsHiddenWeekends ? new List<int> { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday } : new List<int>();

        public bool IsHiddenWeekends { get; set; }
    }
}
