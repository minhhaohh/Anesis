using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<ProcedureViewModel>>> SearchProceduresAsync(
            ProcedureFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ProcedureViewModel>>>(
                $"{API_Procedures}?{model.ToQueryParams()}", cancellationToken);
        }
    }
}
