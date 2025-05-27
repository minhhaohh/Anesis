using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.Web.Data.Models
{
    public class StaffScheduleFilterModel
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ResourceType { get; set; }

        public List<int> ResourceIds { get; set; }

        public string ResourceIdList 
            => ResourceIds != null && ResourceIds.Count > 0
                ? string.Join(",", ResourceIds)
                : string.Empty;

        public List<int> HiddenDaysOfWeek 
            => IsHiddenWeekends ? new List<int> { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday } : new List<int>();

        public bool IsHiddenWeekends { get; set; }
    }

    public class StaffScheduleEditModel
    {
        public int? Id { get; set; }

        public int? LocationId { get; set; }

        public List<int> EmployeeIds { get; set; } = new();

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<int> DaysOfWeek { get; set; } = new();

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string SaveOption { get; set; } = StaffScheduleSaveOption.ShowErrors;

        public string Notes { get; set; }

        public StaffScheduleEditModel()
        {
            var weekends = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };
            DaysOfWeek = typeof(DayOfWeek).GetEnumValues()
                .Cast<int>()
                .Where(x => !weekends.Contains(x))
                .ToList();
        }
    }

    public class StaffScheduleDeleteModel
    {
        public int? Id { get; set; }

        public int? LocationId { get; set; }

        public string EmployeeIdList
            => EmployeeIds.Count > 0
                ? EmployeeIds.StrJoin(",")
                : string.Empty;

        public List<int> EmployeeIds { get; set; } = new();

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string DaysOfWeekList
            => DaysOfWeek.Count > 0
                ? DaysOfWeek.StrJoin(",")
                : string.Empty;

        public List<int> DaysOfWeek { get; set; } = new();
    }
}
