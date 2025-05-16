using Anesis.ApiService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore
{
    public class AccountStore : UserStore<Account, Role, AnesisContext, string, UserClaim, UserInRole, UserLogin, UserToken, RoleClaim>
    {
        public AccountStore(AnesisContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
