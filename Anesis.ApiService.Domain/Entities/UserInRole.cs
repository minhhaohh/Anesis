using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Anesis.ApiService.Domain.Entities
{
    public class UserInRole : IdentityUserRole<string>
    {
        [JsonIgnore]
        public Role Role { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
    }
}
