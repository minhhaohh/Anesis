using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.Entities
{
    public class GeneralCategory : IEntity, IActivatable, IDisplayOrder
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public CategoryUses Uses { get; set; }

        public int? ParentId { get; set; }

        public string BgColor { get; set; }

        public string ForeColor { get; set; }

        public string IconName { get; set; }

        public string Notes { get; set; }


        public GeneralCategory ParentCategory { get; set; }
    }
}
