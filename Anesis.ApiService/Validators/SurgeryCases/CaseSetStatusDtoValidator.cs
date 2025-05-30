using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Domain.DTOs.SurgeryCases;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using FluentValidation;

namespace Anesis.ApiService.Validators.SurgeryCases
{
    public class CaseSetStatusDtoValidator : AbstractValidator<CaseSetStatusDto>
    {
        private ISurgeryCaseService _surgeryCaseService;

        public CaseSetStatusDtoValidator(ISurgeryCaseService surgeryCaseService) 
        {
            _surgeryCaseService = surgeryCaseService; 
            
            RuleFor(x => x.Id)
                .MustAsync(SurgeryCaseExisted)
                .WithMessage(x => $"Could not find surgery case with ID #{x.Id}.");

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("The Status field is required.")
                .Must(AllowableStatus)
                .WithMessage(x => $"The Status #{x.Status} is not allowable.");

            RuleFor(x => x.Reason)
                .Must(HasReasonIfCancel)
                .WithMessage("The Reason field is required.");
        }

        private async Task<bool> SurgeryCaseExisted(int id, CancellationToken cancellationToken)
        {
            return await _surgeryCaseService.GetByIdAsync(id, cancellationToken) != null;
        }

        private bool AllowableStatus(int? status)
        {
            int[] validStatus =
            [
                (int)SurgeryCaseStatus.Cancelled,
                (int)SurgeryCaseStatus.Completed
            ];

            return status == null || validStatus.Contains(status.Value);
        }

        private bool HasReasonIfCancel(CaseSetStatusDto model, string reason)
        {
            if (model.Status != (int)SurgeryCaseStatus.Cancelled)
            {
                return true;
            }

            return reason.HasValue();
        }
    }
}
