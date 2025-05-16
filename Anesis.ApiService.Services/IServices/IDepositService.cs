using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Deposits;

namespace Anesis.ApiService.Services.IServices
{
    public interface IDepositService
    {
        Task<IPagedList<DepositDto>> SearchAsync(
            DepositFilterDto filter, CancellationToken cancellationToken = default);

        Task<DepositDto> GetByIdAsync(int id, bool includeDetails, CancellationToken cancellationToken = default);

        Task<DepositDto> GetByBankIdAsync(int bankId, bool includeDetails, CancellationToken cancellationToken = default);

        Task<List<DepositDto>> GetByBankIdsAsync(List<int> bankIds, CancellationToken cancellationToken = default);

        Task<List<DepositItemDto>> GetDepositItemsAsync(int depositId, CancellationToken cancellationToken = default);
    }
}
