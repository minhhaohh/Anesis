using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Services.IServices
{
    public interface IBatchTransactionService
    {
        Task<IPagedList<BatchTransactionDto>> SearchAsync(
            BatchTransactionFilterDto filter, CancellationToken cancellationToken = default);

        Task<List<BatchTransactionDto>> GetByBankIdAsync(int bankId, CancellationToken cancellationToken = default);

        Task<List<BatchTransactionDto>> GetByBankIdsAsync(List<int> bankIds, CancellationToken cancellationToken = default);

        Task<BatchTransactionDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<List<int>> GetAutoLinkedItemIdsByBankId(int bankId, CancellationToken cancellationToken = default);

        Task<bool> LinkBankIdAsync(BatchLinkBankIdDto model, CancellationToken cancellationToken = default);

        Task<bool> RemoveLinkedBankIdAsync(int batchId, CancellationToken cancellationToken = default);

        Task<bool> CreateAdjustmentAsync(BatchAdjustmentCreateDto model, CancellationToken cancellationToken = default);

        Task<bool> SplitAsync(BatchTransactionSplitDto model, CancellationToken cancellationToken = default);
    }
}
