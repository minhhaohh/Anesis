using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.Menus
{
    public class QuickLinkEditDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MenuItemId { get; set; }

        public int DisplayOrder { get; set; }

        public string GroupName { get; set; }

        public UserQuickLink ToQuickLinkModel()
        {
            return new UserQuickLink
            {
                UserId = UserId,
                MenuItemId = MenuItemId,
                DisplayOrder = DisplayOrder,
                GroupName = GroupName
            };
        }

        public void ApplyChangesTo(UserQuickLink quickLink)
        {
            quickLink.MenuItemId = MenuItemId;
            quickLink.DisplayOrder = DisplayOrder;
            quickLink.GroupName = GroupName;
        }
    }
}
