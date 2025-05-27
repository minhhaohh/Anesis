namespace Anesis.ApiService.Domain.DTOs.Timetables
{
    public class CalendarEventDto
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Start => StartTime.ToString("s");

        public string End => EndTime.ToString("s");

        public bool AllDay { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public bool IsProvider { get; set; }

        public bool? Editable { get; set; }

        public bool? StartEditable { get; set; }

        public bool? DurationEditable { get; set; }

        public int DisplayOrder { get; set; }

        public object ExtendedProps { get; set; }
    }
}
