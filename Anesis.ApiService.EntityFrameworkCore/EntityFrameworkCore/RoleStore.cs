using Anesis.ApiService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public class RoleStore : RoleStore<Role, AnesisContext, string, UserInRole, RoleClaim>
    {
        public RoleStore(AnesisContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
