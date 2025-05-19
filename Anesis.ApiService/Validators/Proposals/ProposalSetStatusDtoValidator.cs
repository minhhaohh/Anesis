using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using FluentValidation;

namespace Anesis.ApiService.Validators.Proposals
{
    public class ProposalSetStatusDtoValidator : AbstractValidator<ProposalSetStatusDto>
    {
        private readonly IProposalService _proposalService;

        public ProposalSetStatusDtoValidator(IProposalService proposalService)
        {
            _proposalService = proposalService;

            RuleFor(x => x.Id)
                .MustAsync(ProposalExistedAsync)
                .WithMessage(x => $"Could not find proposal with ID #{x.Id}.");

            RuleFor(x => x.Status)
                .NotNull()
                .WithMessage("The Status field is required.")
                .Must(AllowableStatus)
                .WithMessage(x => $"The Status #{x.Status} is not allowable.");

            RuleFor(x => x.Reason)
                .Must(CancellationWithReason)
                .WithMessage("The Reason field is required.");
        }

        private async Task<bool> ProposalExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _proposalService.GetByIdAsync(id, cancellationToken) != null;
        }

        private bool AllowableStatus(int? status)
        {
            int[] validStatus =
            [
                (int)PotentialProcedureStatus.Ordered,
                (int)PotentialProcedureStatus.Cancelled,
                (int)PotentialProcedureStatus.Completed
            ];

            return status == null || validStatus.Contains(status.Value);
        }

        private bool CancellationWithReason(ProposalSetStatusDto model, string reason)
        {
            if (model.Status != (int)PotentialProcedureStatus.Cancelled)
            {
                return true;
            }

            return reason.HasValue();
        }
    }
}
