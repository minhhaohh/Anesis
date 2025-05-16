using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class EncounterType : IEntity, IActivatable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Shorthand { get; set; }

        public decimal? AverageAmount { get; set; }

        public decimal? LabAmount { get; set; }

        public int Points { get; set; }

        public int? ForecastCountThreshold { get; set; }

        public string SourceSite { get; set; }

        public decimal? SascAverageAmount { get; set; }

        public string Category { get; set; }

        public bool IsActive { get; set; }

        public bool IsProcedure { get; set; }

        public int? EcwSiteId { get; set; }

        public int? MapToId { get; set; }

        public int? MapToEncId { get; set; }
    }
}
