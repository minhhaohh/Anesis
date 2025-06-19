namespace Anesis.ApiService.Domain.DTOs.Menus
{
    public class MenuItemDto
    {
        public int Id { get; set; }

        public int MenuTabId { get; set; }

        public string TabName { get; set; }
        
        public int TabDisplayOrder { get; set; }

        public string TabIconPath { get; set; }

        public string MenuText { get; set; }

        public string LinkUrl { get; set; }

        public string Tooltip { get; set; }

        public string IconPath { get; set; }

        public string CssClass { get; set; }

        public int DisplayOrder { get; set; }
    }
}
