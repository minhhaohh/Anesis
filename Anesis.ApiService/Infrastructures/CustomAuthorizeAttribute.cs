using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Anesis.ApiService.Infrastructures
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute()
        {
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
        }
    }
}
