using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Permission : IEntity, IDisplayOrder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int FlagValue { get; set; }

        public int ModuleId { get; set; }

        // TODO: Map to SortOrder
        public int DisplayOrder { get; set; }

        public AppModule Module { get; set; }
    }
}
