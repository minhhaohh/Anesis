using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Accounts;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Anesis.ApiService.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Account> _userManger;

        public AuthService(UserManager<Account> userManager)
        {
            _userManger = userManager;
        }

        public async Task<bool> CheckUserNamePasswordAsync(AccountLoginDto model, CancellationToken cancellationToken = default)
        {
            var account = await _userManger.FindByNameAsync(model.UserName);

            if (account == null)
            {
                return false;
            }

            return await _userManger.CheckPasswordAsync(account, model.Password);
        }
    }
}
