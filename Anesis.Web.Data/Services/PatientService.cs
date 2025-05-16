using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<PatientViewModel>> GetPatientByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<PatientViewModel>>($"{API_Patients}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<PatientViewModel>> GetPatientByChartNoAsync(
            string chartNo, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<PatientViewModel>>($"{API_Patients}/ByChartNo/{chartNo}", cancellationToken);
        }
    }
}
