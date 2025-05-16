using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.Entities
{
    public class Timetable : IEntity
    {
        public int Id { get; set; }

        public DateTime CalendarDate { get; set; }

        public int LocationId { get; set; }

        public int? ResourceId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public ScheduleEventType EventType { get; set; }

        public string Tooltip { get; set; }

        public int? EmployeeId { get; set; }

        public int? EntityId { get; set; }

        public string EntityType { get; set; }

        public string Notes { get; set; }


        public Location Location { get; set; }

        public Employee Employee { get; set; }


        public double DurationInHours => (EndTime - StartTime).TotalHours;

        public double DurationInMinutes => (EndTime - StartTime).TotalMinutes;
    }
}
