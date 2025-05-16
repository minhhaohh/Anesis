using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class StaffScheduleEditDto
    {
        public int? Id { get; set; }

        public int? LocationId { get; set; }

        public List<int> EmployeeIds { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<int> DaysOfWeek { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string SaveOption { get; set; }

        public string Notes { get; set; }

        public bool IsSingleEvent()
        {
            return Id > 0 && EmployeeIds.Count == 1 && (ToDate == null || ToDate == FromDate);
        }

        public List<Timetable> ToTimetables()
        {
            var timetables = new List<Timetable>();

            for (var scheduleDate = FromDate.Value; scheduleDate <= (ToDate ?? FromDate.Value); scheduleDate = scheduleDate.AddDays(1))
            {
                if (ToDate != null && !DaysOfWeek.Contains((int)scheduleDate.DayOfWeek))
                    continue;

                var schedules = EmployeeIds.Select(x => new Timetable()
                {
                    CalendarDate = scheduleDate,
                    LocationId = LocationId.Value,
                    EmployeeId = x,
                    StartTime = StartTime.Value,
                    EndTime = EndTime.Value,
                    EventType = ScheduleEventType.StaffSchedule,
                    Notes = Notes
                });

                timetables.AddRange(schedules);
            }

            return timetables;
        }

        public void ApplyToChanges(Timetable timetable)
        {
            timetable.CalendarDate = FromDate.Value;
            timetable.LocationId = LocationId.Value;
            timetable.EmployeeId = EmployeeIds.First();
            timetable.StartTime = StartTime.Value;
            timetable.EndTime = EndTime.Value;
            timetable.Notes = Notes;
        }

        public string GetModifiedFields(Timetable timetable)
        {
            var fields = new List<string>();

            if (FromDate != timetable.CalendarDate)
                fields.Add(nameof(Timetable.CalendarDate));
            if (LocationId != timetable.LocationId)
                fields.Add(nameof(timetable.LocationId));
            if (EmployeeIds.First() != timetable.EmployeeId)
                fields.Add(nameof(Timetable.EmployeeId));
            if (StartTime != timetable.StartTime)
                fields.Add(nameof(Timetable.StartTime));
            if (EndTime != timetable.EndTime)
                fields.Add(nameof(Timetable.EndTime));
            if (Notes != timetable.Notes)
                fields.Add(nameof(Timetable.Notes));

            return fields.StrJoin(",");
        }
    }
}
