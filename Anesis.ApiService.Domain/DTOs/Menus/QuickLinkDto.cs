namespace Anesis.ApiService.Domain.DTOs.Menus
{
    public class QuickLinkDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MenuItemId { get; set; }

        public string MenuText { get; set; }

        public string LinkUrl { get; set; }

        public string Tooltip { get; set; }

        public string IconPath { get; set; }

        public string CssClass { get; set; }

        public int DisplayOrder { get; set; }

        public string GroupName { get; set; }
    }
}
