using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class UserQuickLink : IEntity, IDisplayOrder
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int MenuItemId { get; set; }

        public int DisplayOrder { get; set; }

        public string GroupName { get; set; }

        public Account Account { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
