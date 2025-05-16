using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class GroupPermission : IEntity
    {
        public int Id { get; set; }

        public int UserGroupId { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }

        public UserGroup UserGroup { get; set; }
    }
}
