namespace Anesis.ApiService.Domain.DTOs.Menus
{
    public class MenuItemFilterDto
    {
        public int? MenuTabId { get; set; }

        public bool VisibleOnly { get; set; }

        public List<int> PermissionIds { get; set; } = new();
    }
}
