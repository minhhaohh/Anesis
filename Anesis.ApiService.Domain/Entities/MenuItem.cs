using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class MenuItem : IEntity, IDisplayOrder
    {
        public int Id { get; set; }

        public string MenuText { get; set; }

        public string LinkUrl { get; set; }

        public string Tooltip { get; set; }

        public string IconPath { get; set; }

        public string CssClass { get; set; }

        public int DisplayOrder { get; set; }

        public bool Visible { get; set; }

        public string Notes { get; set; }

        public int MenuTabId { get; set; }

        public int? PermissionId { get; set; }

        public string NewLinkUrl { get; set; }

        public string NewIconPath { get; set; }

        public MenuTab MenuTab { get; set; }

        public Permission Permission { get; set; }
    }
}
