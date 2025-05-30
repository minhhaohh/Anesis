using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<DeviceWithCostViewModel>>> SearchDevicesAsync(
            DeviceFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<DeviceWithCostViewModel>>>(
                $"{API_Devices}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<List<DeviceWithCostViewModel>>> GetAllDevicesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<DeviceWithCostViewModel>>>(
                $"{API_Devices}/All?activeOnly={activeOnly}", cancellationToken);
        }

        public async Task<ResponseModel<DeviceWithCostViewModel>> GetDeviceByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<DeviceWithCostViewModel>>($"{API_Devices}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<DeviceCostViewModel>> GetCurrentDeviceCostAsync(
            int deviceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<DeviceCostViewModel>>($"{API_Devices}/CurrentCost/{deviceId}", cancellationToken);
        }

        public async Task<ResponseModel<List<DeviceCostViewModel>>> GetDevicePriceHistoryAsync(
            int deviceId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<DeviceCostViewModel>>>($"{API_Devices}/PriceHistory/{deviceId}", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateDeviceAsync(
            DeviceWithCostEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Devices}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateDeviceAsync(
            DeviceWithCostEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Devices}/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateDeviceFieldAsync(
            FieldUpdateModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Devices}/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> ToggleDeviceFlagAsync(
            FlagToggleModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Devices}/ToggleFlag/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateDeviceCostAsync(
            DeviceCostEditModel deviceCost, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Devices}/Costs/{deviceCost.DeviceId}", deviceCost, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
