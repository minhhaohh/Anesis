using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.Devices
{
    public class DeviceCostEditDtoValidator : AbstractValidator<DeviceCostEditDto>
    {
        private readonly IDeviceService _deviceService;

        public DeviceCostEditDtoValidator(IDeviceService deviceService)
        {
            _deviceService = deviceService;

            RuleFor(x => x.DeviceId)
                .MustAsync(DeviceExistedAsync)
                .WithMessage(x => $"Could not find device with ID #{x.DeviceId}.");

            RuleFor(x => x.AppliedCost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Total Cost field must be greater than or equal to 0.");

            RuleFor(x => x.EffectiveDate)
                .NotNull()
                .WithMessage("The Effective Date field is required.")
                .MustAsync(NewEffDateMustBeAfterPreEffDateAsync)
                .WithMessage("New effective date must be after the previous effective date.");
        }

        private async Task<bool> DeviceExistedAsync(int deviceId, CancellationToken cancellationToken)
        {
            return await _deviceService.GetByIdAsync(deviceId, cancellationToken) != null;
        }

        private async Task<bool> NewEffDateMustBeAfterPreEffDateAsync(
            DeviceCostEditDto model, DateTime? effDate, CancellationToken cancellationToken)
        {
            if (effDate == null || model.OverwriteEffDate) 
                return true;

            var deviceCost = await _deviceService.GetCurrentCostAsync(model.DeviceId, cancellationToken);
            return effDate.Value.Date >= deviceCost?.EffectiveDate.Date;
        }
    }
}
