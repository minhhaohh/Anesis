using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class AppModule : IEntity, IDisplayOrder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // TODO: Map to SortOrder
        public int DisplayOrder { get; set; }

        public string Notes { get; set; }
    }
}
