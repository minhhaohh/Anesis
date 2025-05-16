using Anesis.ApiService.Domain.DTOs.Accounts;

namespace Anesis.ApiService.Services.IServices
{
    public interface IAuthService
    {
        Task<bool> CheckUserNamePasswordAsync(AccountLoginDto model, CancellationToken cancellationToken = default);
    }
}
