using Anesis.Web.Data.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Anesis.Web.Data.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IApiService _apiService;

        public CustomAuthStateProvider(IApiService apiService)
        {
            _apiService = apiService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var response = await _apiService.GetLoggedUserInfoAsync();

            if (response.Success)
            {
                var claims = new List<Claim>() 
                { 
                    new Claim(ClaimTypes.Name, response.Data.UserName), 
                    new Claim(ClaimTypes.Expiration, response.Data.ExpirationUtcDate.ToString()) 
                };
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom Authentication"));

                return new AuthenticationState(authenticatedUser);
            }

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            return new AuthenticationState(anonymousUser);
        }
    }
}
