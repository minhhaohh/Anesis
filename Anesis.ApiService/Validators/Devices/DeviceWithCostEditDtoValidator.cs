using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.Devices
{
    public class DeviceWithCostEditDtoValidator : AbstractValidator<DeviceWithCostEditDto>
    {
        private readonly IDeviceService _deviceService;

        public DeviceWithCostEditDtoValidator(IDeviceService deviceService) 
        {
            _deviceService = deviceService;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The Device Name field is required.");

            RuleFor(x => x.Category)
                .NotNull()
                .WithMessage("The Category field is required.");

            RuleFor(x => x.DisplayOrder)
                .NotNull()
                .WithMessage("The Display Order field is required.")
                .GreaterThan(0)
                .WithMessage("The Display Order field must be greater than 0.");

            //RuleFor(x => x.VendorCost)
            //	.GreaterThanOrEqualTo(0)
            //	.WithMessage("Base cost must be greater than or equal to 0");

            RuleFor(x => x.AppliedCost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Total Cost field must be greater than or equal to 0.");

            RuleFor(x => x.EffectiveDate)
                .NotNull()
                .WithMessage("The Effective Date field is required.");

            When(x => x.Id > 0, () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(DeviceExistedAsync)
                    .WithMessage(x => $"Could not find device with ID #{x.Id}.");

                RuleFor(x => x.EffectiveDate)
                    .MustAsync(NewEffDateMustBeAfterPreEffDateAsync)
                    .WithMessage("The New Effective Date must be after the Previous Effective Date.");
            });
        }

        public async Task<bool> DeviceExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _deviceService.GetByIdAsync(id, cancellationToken) != null;
        }

        public async Task<bool> NewEffDateMustBeAfterPreEffDateAsync(
            DeviceWithCostEditDto model, DateTime? effDate, CancellationToken cancellationToken)
        {
            if (effDate == null || model.OverwriteEffDate)
                return true;

            var deviceCost = await _deviceService.GetCurrentCostAsync(model.Id, cancellationToken);

            return effDate.Value.Date >= deviceCost?.EffectiveDate.Date;
        }
    }
}
