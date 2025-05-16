using Anesis.ApiService.Domain.DTOs.SurgeryCases;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.SurgeryCases
{
    public class CaseCostAddDtoValidator : AbstractValidator<CaseCostAddDto>
    {
        private readonly ISurgeryCaseService _surgeryCaseService;
        private readonly ICustomerService _deviceService;

        public CaseCostAddDtoValidator(
            ISurgeryCaseService surgeryCaseService,
            ICustomerService deviceService) 
        {
            _surgeryCaseService = surgeryCaseService;
            _deviceService = deviceService;

            RuleFor(x => x.CaseId)
                .NotNull()
                .WithMessage("The Surgery Case field is required.")
                .MustAsync(CaseExistedAsync)
                .WithMessage(x => $"Could not find surgery case with ID #{x.CaseId}.");

            RuleFor(x => x.DeviceId)
                .NotNull()
                .WithMessage("The Device field is required.")
                .MustAsync(DeviceExistedAsync)
                .WithMessage(x => $"Could not find device with ID #{x.CaseId}.");

            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("The Quantity field is required.")
                .GreaterThan(0)
                .WithMessage("The Quantity must be greater than 0.");
        }

        private async Task<bool> CaseExistedAsync(int? caseId, CancellationToken cancellationToken)
        {
            return caseId == null || await _surgeryCaseService.GetByIdAsync(caseId.Value, cancellationToken) != null;
        }

        private async Task<bool> DeviceExistedAsync(int? deviceId, CancellationToken cancellationToken)
        {
            return deviceId == null || await _deviceService.GetByIdAsync(deviceId.Value, cancellationToken) != null;
        }
    }
}
