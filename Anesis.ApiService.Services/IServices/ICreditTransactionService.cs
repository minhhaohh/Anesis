using Anesis.ApiService.Domain.DTOs.CreditTransactions;

namespace Anesis.ApiService.Services.IServices
{
    public interface ICreditTransactionService
    {
        Task<List<CreditTransactionDto>> GetByBankIdAsync(int bankId, CancellationToken cancellationToken = default);
    }
}
