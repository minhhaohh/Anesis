using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class MenuTab : IEntity, IDisplayOrder
    {
        public int Id { get; set; }

        public string TabName { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        public string IconPath { get; set; }

        public string Notes { get; set; }

        public string NewIconPath { get; set; }

        public List<MenuItem> MenuItems { get; set; }
    }
}
