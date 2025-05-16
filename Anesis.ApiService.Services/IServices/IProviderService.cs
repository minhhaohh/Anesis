using Anesis.Shared.Models;
using Anesis.ApiService.Domain.DTOs.Providers;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Services.IServices
{
    public interface IProviderService
    {
        Task<IPagedList<Provider>> SearchAsync(
            ProviderFilterDto filter, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetDoctorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetMidLevelProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);
    }
}
