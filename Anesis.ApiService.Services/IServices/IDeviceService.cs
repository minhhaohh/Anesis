using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Devices;

namespace Anesis.ApiService.Services.IServices
{
    public interface IDeviceService
    {
        Task<IPagedList<DeviceWithCostDto>> SearchAsync(
            DeviceFilterDto filter, CancellationToken cancellationToken = default);

        Task<List<DeviceWithCostDto>> GetAllAsync(bool activeOnly = false,
            CancellationToken cancellationToken = default);

        Task<DeviceWithCostDto> GetByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<DeviceCostDto> GetCurrentCostAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<List<DeviceCostDto>> GetPriceHistoryAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<bool> ToggleFlagAsync(
            FlagToggleDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateDeviceCostAsync(
            DeviceCostEditDto model, CancellationToken cancellationToken = default);

        Task<bool> CreateDeviceAsync(
            DeviceWithCostEditDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateDeviceAsync(
            DeviceWithCostEditDto model, CancellationToken cancellationToken = default);
    }
}
