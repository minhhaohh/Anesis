using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.Proposals
{
    public class ProposalReviewDtoValidator : AbstractValidator<ProposalReviewDto>
    {
        private readonly IProposalService _proposalService;

        public ProposalReviewDtoValidator(IProposalService potentialProcService)
        {
            _proposalService = potentialProcService;

            RuleFor(x => x.ProcedureId)
                .NotNull()
                .WithMessage("The Proposed Procedure field is required.");

            RuleFor(x => x.DiagnosisCode)
                .NotEmpty()
                .WithMessage("The Diagnosis Code field is required.");

            RuleFor(x => x.ReviewerNotes)
                .NotEmpty()
                .WithMessage("The Reason field is required.");

            When(x => x.Id > 0, () =>
            {
                RuleFor(x => x.Id)
                    .MustAsync(ProposalExistedAsync)
                    .WithMessage(x => $"Could not find proposal with ID #{x.Id}.");
            });

        }

        private async Task<bool> ProposalExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _proposalService.GetByIdAsync(id, cancellationToken) != null;
        }
    }
}
