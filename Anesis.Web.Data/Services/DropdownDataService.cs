using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Locations?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownAscLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/AscLocations?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownEmployeesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Employees?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Providers?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownDoctorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Doctors?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownMidLevelProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/MidLevelProviders?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownProceduresAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Procedures?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<Dictionary<int, string>>> GetDropdownInsurancesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<Dictionary<int, string>>>(
                $"{API_Dropdown_Data}/Insurances?activeOnly={activeOnly}");
        }

        public async Task<ResponseModel<List<string>>> GetDropdownInvoiceOwnersAsync(
            CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<string>>>(
                $"{API_Dropdown_Data}/InvoiceOwners");
        }
    }
}
