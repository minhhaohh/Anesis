using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalReviewDto
    {
        public int Id { get; set; }

        public int? ProcedureId { get; set; }

        public string DiagnosisCode { get; set; }

        public string ReviewerNotes { get; set; }

        public bool IsApproved { get; set; }

        public void ApplyChangesTo(PotentialProcedure proposal)
        {
            var currentTime = DateTime.Now;

            proposal.ProcedureId = ProcedureId.Value;
            proposal.DiagnosisCode = DiagnosisCode;
            proposal.RequestStatus = IsApproved ? PotentialProcedureStatus.Reviewed : PotentialProcedureStatus.Cancelled;
            proposal.ReviewerNotes = ReviewerNotes;
            proposal.ReviewedDate = currentTime;
            proposal.UpdatedBy = "haotm";
            proposal.UpdatedDate = currentTime;
        }
    }
}
