using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Devices;

namespace Anesis.ApiService.Services.IServices
{
    public interface IDeviceService
    {
        Task<IPagedList<DeviceWithCostDto>> SearchAsync(
            DeviceFilterDto filter, CancellationToken cancellationToken = default);

        Task<List<DeviceWithCostDto>> GetAllAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<DeviceWithCostDto> GetByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<DeviceCostDto> GetCurrentCostAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<List<DeviceCostDto>> GetPriceHistoryAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<bool> UpdateFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default);

        Task<bool> ToggleFlagAsync(FlagToggleDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateCostAsync(DeviceCostEditDto model, CancellationToken cancellationToken = default);

        Task<bool> CreateAsync(DeviceWithCostEditDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(DeviceWithCostEditDto model, CancellationToken cancellationToken = default);
    }
}
