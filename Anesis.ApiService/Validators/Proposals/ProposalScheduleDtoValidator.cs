using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.Proposals
{
    public class ProposalScheduleDtoValidator : AbstractValidator<ProposalScheduleSurgeryDto>
    {
        private readonly IProposalService _proposalService;

        public ProposalScheduleDtoValidator(IProposalService potentialProcService)
        {
            _proposalService = potentialProcService;

            RuleFor(x => x.Id)
                .MustAsync(ProposalExistedAsync)
                .WithMessage(x => $"Could not find proposal with ID #{x.Id}.");

            RuleFor(x => x.SurgeonId)
                .NotNull()
                .WithMessage("The Surgeon field is required.");

            RuleFor(x => x.SurgeryLocationId)
                .NotNull()
                .WithMessage("The Surgery Location field is required.");

            RuleFor(x => x.SurgeryDate)
                .NotNull()
                .WithMessage("The Surgery Date field is required.");

            RuleFor(x => x.SurgeryTime)
                .NotNull()
                .WithMessage("The Surgery Time field is required.");
        }

        private async Task<bool> ProposalExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _proposalService.GetByIdAsync(id, cancellationToken) != null;
        }
    }
}
