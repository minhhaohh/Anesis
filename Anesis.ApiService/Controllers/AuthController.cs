using Anesis.ApiService.Domain.DTOs.Accounts;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IConfiguration config,
            IAuthService authService)
        {
            _logger = logger;
            _config = config;
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<Result> Login([FromBody]AccountLoginDto model)
        {
            var validator = new AccountLoginDtoValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _authService.CheckUserNamePasswordAsync(model))
            {
                return Result.Error("Invalid username or password.");
            }

            var expirationDays = model.RememberMe
                ? _config.GetValue<int>("LongExpirationInDays")
                : _config.GetValue<int>("ExpirationInDays");
            var expirationUtcDate = DateTime.UtcNow.AddDays(expirationDays); 

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Expiration, expirationUtcDate.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = expirationUtcDate,
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            var response = new LoginResponseDto()
            {
                UserName = model.UserName,
                ExpirationUtcDate = expirationUtcDate
            };

            return Result.Ok(response);
        }

        [HttpGet("Info")]
        public Result Info()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Result.Error("Unauthenticated.");
            }

            var currentUtcDate = DateTime.UtcNow;
            var expirationUtcDate = DateTime.Parse(User.FindFirst(ClaimTypes.Expiration).Value);

            if (currentUtcDate > expirationUtcDate)
            {
                return Result.Error("Session expired.");
            }

            var response = new LoginResponseDto()
            {
                UserName = User.Identity.Name,
                ExpirationUtcDate = expirationUtcDate
            };

            return Result.Ok(response);
        }

        [HttpPost("Logout")]
        public async Task<Result> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Result.Ok(true);
        }

        [HttpGet("HasPermission/{id}")]
        public async Task<Result> CheckHasPermission(int id)
        {
            return Result.Ok(true);
        }
    }
}
