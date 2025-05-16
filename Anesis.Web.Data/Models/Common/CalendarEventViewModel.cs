using Anesis.Shared.Extensions;
using System.Text.Json;

namespace Anesis.Web.Data.Models.Common
{
    public class CalendarEventViewModel
    {
        public int Id { get; set; }

        public int ResourceId { get; set; }

        public string Title { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public bool AllDay { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        public bool IsProvider { get; set; }

        public bool? Editable { get; set; }

        public bool? StartEditable { get; set; }

        public bool? DurationEditable { get; set; }

        public string ClassName { get; set; }

        public object ExtendedProps { get; set; }

        public string GetExtendProperty(string propName)
        {
            if (ExtendedProps is JsonElement jsonElement)
            {
                if (jsonElement.TryGetProperty(propName, out var property))
                {
                    return property.ToString();
                }

                // Handle case-insensitive property lookup
                foreach (var element in jsonElement.EnumerateObject())
                {
                    if (string.Equals(element.Name, propName, StringComparison.OrdinalIgnoreCase))
                    {
                        return element.Value.ToString();
                    }
                }
            }

            return null;
        }

        public StaffScheduleEditModel ToStaffScheduleEditModel()
        {
            var employeeId = GetExtendProperty("EmployeeId");
            var employeeIds = employeeId == null ? new List<int>() : new List<int>() { employeeId.ToInt() };

            var StartDateTime = DateTime.Parse(Start);
            var EndDateTime = DateTime.Parse(End);

            var model = new StaffScheduleEditModel
            {
                Id = Id,
                FromDate = StartDateTime.Date,
                ToDate = EndDateTime.Date,
                EmployeeIds = employeeIds,
                LocationId = ResourceId,
                StartTime = StartDateTime.TimeOfDay,
                EndTime = EndDateTime.TimeOfDay,
                Notes = GetExtendProperty("Notes")
            };
            return model;
        }
    }
}
