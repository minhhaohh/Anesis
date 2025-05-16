using Anesis.ApiService.Domain.DTOs.Accounts;

namespace Anesis.ApiService.Services.IServices
{
    public interface IAccountService
    {
        Task<AccountDto> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}
