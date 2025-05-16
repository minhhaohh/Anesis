using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using FluentValidation;

namespace Anesis.ApiService.Validators.Proposals
{
    public class ProposalEditDtoValidator : AbstractValidator<ProposalEditDto>
    {
        private readonly IProposalService _proposalService;

        public ProposalEditDtoValidator(IProposalService potentialProcService)
        {
            _proposalService = potentialProcService;

            RuleFor(x => x.PatientId)
                .NotNull()
                .WithMessage("The Patient field is required.");

            RuleFor(x => x.ProviderId)
                .NotNull()
                .WithMessage("The Appointment Provider field required.");

            RuleFor(x => x.LocationId)
                .NotNull()
                .WithMessage("The Appointment Location field required.");

            RuleFor(x => x.ProcedureId)
                .NotNull()
                .WithMessage("The Proposed Procedure field required.");

            RuleFor(x => x.DiagnosisCode)
                .NotEmpty()
                .WithMessage("The Diagnosis Code field is required required.");

            When(x => x.Id > 0, () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(ProposalExistedAsync)
                    .WithMessage(x => $"Could not find proposal with ID #{x.Id}.")
                    .MustAsync(StatusBeforeOrderedAsync)
                    .WithMessage(x => $"The status of proposal #{x.Id} must be Pending or Reviewed.");

                RuleFor(x => x.ReasonChange)
                    .NotEmpty()
                    .WithMessage("The Reason For Change field is required.");
            });
        }

        private async Task<bool> ProposalExistedAsync(int proposalId, CancellationToken cancellationToken)
        {
            return await _proposalService.GetByIdAsync(proposalId, cancellationToken) != null;
        }

        private async Task<bool> StatusBeforeOrderedAsync(int proposalId, CancellationToken cancellationToken)
        {
            var proposal = await _proposalService.GetByIdAsync(proposalId, cancellationToken);

            return proposal == null || proposal.RequestStatus < PotentialProcedureStatus.Ordered;
        }
    }
}
