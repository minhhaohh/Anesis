using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;

namespace Anesis.Web.Data.Services
{
    public interface IDeviceService
    {
        Task<ResponseModel<List<DeviceWithCostViewModel>>> SearchDevicesAsync(
            DeviceFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<DeviceWithCostViewModel>>> GetAllDevicesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<DeviceWithCostViewModel>> GetDeviceByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<DeviceCostViewModel>> GetCurrentDeviceCostAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<DeviceCostViewModel>>> GetDevicePriceHistoryAsync(
            int deviceId, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateDeviceAsync(
            DeviceWithCostEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateDeviceAsync(
            DeviceWithCostEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ToggleDeviceFlagAsync(
            FlagToggleModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateDeviceCostAsync(
            DeviceCostEditModel model, CancellationToken cancellationToken = default);
    }
}
