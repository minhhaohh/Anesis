namespace Anesis.ApiService.Domain.Entities
{
    public class UserInGroup
    {
        public int Id { get; set; }

        public string AccountId { get; set; }

        public int UserGroupId { get; set; }

        public Account Account { get; set; }

        public UserGroup UserGroup { get; set; }
    }
}
