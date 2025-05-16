using Anesis.ApiService.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Anesis.ApiService.Domain.Entities
{
    public class Role : IdentityRole<string>, IDisplayOrder
    {
        /// <summary>Constructor</summary>
        public Role()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>Constructor</summary>
        /// <param name="roleName"></param>
        public Role(string roleName) : this()
        {
            this.Name = roleName;
        }

        public int DisplayOrder { get; set; }
    }
}
