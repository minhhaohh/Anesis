using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalSetStatusDto
    {
        public int Id { get; set; }

        public int? Status { get; set; }

        public string Reason { get; set; }

        public void ApplyChangesTo(PotentialProcedure proposal)
        {
            proposal.RequestStatus = (PotentialProcedureStatus)Status;
            proposal.UpdatedBy = "haotm";
            proposal.UpdatedDate = DateTime.Now;

            if (Reason.HasValue())
            {
                proposal.Notes = $"{proposal.Notes} | {(PotentialProcedureStatus)Status} Reason: {Reason}";
            }
        }
    }
}
