using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Week : IEntity
    {
        public int Id { get; set; }

        public int YearNumber { get; set; }

        public int WeekNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WorkingDays { get; set; }

        public string Notes { get; set; }
    }
}
