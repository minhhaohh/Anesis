using Anesis.Shared.Models;
using Anesis.ApiService.Domain.DTOs.Locations;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Services.IServices
{
    public interface ILocationService
    {
        Task<IPagedList<LocationDto>> SearchAsync(LocationFilterDto filter, 
            CancellationToken cancellationToken = default);

        Task<LocationDto> GetByIdAsync(int id,
            CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetAscLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);
    }
}
