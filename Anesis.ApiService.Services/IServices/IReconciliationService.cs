using Anesis.ApiService.Domain.DTOs.Reconciliations;

namespace Anesis.ApiService.Services.IServices
{
    public interface IReconciliationService
    {
        Task<List<ReconciliationDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<ReconciliationDto> GetLastTimeAsync(CancellationToken cancellationToken = default);

        Task<bool> CreateReconciliation(DateTime ReconciledDate, CancellationToken cancellationToken = default);

        Task<bool> UndoReconciliation(int id, CancellationToken cancellationToken = default);
    }
}
