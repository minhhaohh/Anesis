using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<CustomerViewModel>>> SearchCustomersAsync(
            CustomerFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CustomerViewModel>>>(
                $"{API_Customers}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<List<CustomerViewModel>>> GetAllCustomersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CustomerViewModel>>>(
                $"{API_Customers}/All?activeOnly={activeOnly}", cancellationToken);
        }

        public async Task<ResponseModel<CustomerViewModel>> GetCustomerByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<CustomerViewModel>>($"{API_Customers}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<List<CustomerViewModel>>> SearchVendorsAsync(
            CustomerFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CustomerViewModel>>>(
                $"{API_Customers}/Vendors?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<List<CustomerViewModel>>> GetAllVendorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CustomerViewModel>>>(
                $"{API_Customers}/Vendors/All?activeOnly={activeOnly}", cancellationToken);
        }

        public async Task<ResponseModel<CustomerViewModel>> GetVendorByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<CustomerViewModel>>($"{API_Customers}/Vendors/{id}", cancellationToken);
        }
    }
}
