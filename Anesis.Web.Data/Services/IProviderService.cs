using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IProviderService
    {
        Task<ResponseModel<List<ProviderViewModel>>> SearchProvidersAsync(
            ProviderFilterModel model, CancellationToken cancellationToken = default);
    }
}
