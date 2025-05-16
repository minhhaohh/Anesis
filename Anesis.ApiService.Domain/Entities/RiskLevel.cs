using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class RiskLevel : IEntity
    {
        public int Id { get; set; }

        public string LevelName { get; set; }

        public float IntervalDays { get; set; }
    }
}
