using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class JobRole : IEntity, IActivatable, IDisplayOrder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BranchLocation { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public bool IsManager { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public string Notes { get; set; }
    }
}
