using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Services.IServices
{
    public interface IBankTransactionService
    {
        Task<IPagedList<BankTransactionDto>> SearchAsync(
            BankTransactionFilterDto filter, CancellationToken cancellationToken = default);

        Task<BankTransaction> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<BalanceSummaryDto> GetBalanceSummaryAsync(BankTransactionFilterDto filter, CancellationToken cancellationToken = default);

        Task<bool> CreateAdjustmentAsync(BankAdjustmentCreateDto model, CancellationToken cancellationToken = default);

        Task<bool> SplitAsync(BankTransactionSplitDto model, CancellationToken cancellationToken = default);
    }
}
