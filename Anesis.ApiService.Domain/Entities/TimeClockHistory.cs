using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class TimeClockHistory : IEntity
    {
        public int Id { get; set; }

        public int TimeClockId { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedAction { get; set; }

        public string TimeClockData { get; set; }

        public TimeClock TimeClock { get; set; }
    }
}
