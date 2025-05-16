using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class GeneralChangeLog : IEntity
    {
        public int Id { get; set; }

        public string EntityType { get; set; }

        public int EntityId { get; set; }

        public string ActionName { get; set; }

        public DateTime ActionTime { get; set; }

        public string ChangedBy { get; set; }

        public string UserComment { get; set; }

        public string ChangedFields { get; set; }

        public string BeforeChange { get; set; }

        public string AfterChange { get; set; }

        public bool AdminOnly { get; set; }

        public string Notes { get; set; }
    }
}
