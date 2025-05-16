using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<ProviderViewModel>>> SearchProvidersAsync(
            ProviderFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ProviderViewModel>>>(
                $"{API_Providers}?{model.ToQueryParams()}", cancellationToken);
        }
    }
}
