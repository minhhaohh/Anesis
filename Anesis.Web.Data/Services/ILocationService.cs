using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface ILocationService
    {
        Task<ResponseModel<List<LocationViewModel>>> SearchLocationsAsync(
            LocationFilterModel model, CancellationToken cancellationToken = default);
    }
}
