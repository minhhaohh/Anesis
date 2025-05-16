using Anesis.Shared.Models;

namespace Anesis.ApiService.Services.IServices
{
    public interface IInsuranceService
    {
        Task<Dictionary<int, string>> GetInsurancesAsync(
            bool hasSiteId = false, CancellationToken cancellationToken = default);
    }
}
