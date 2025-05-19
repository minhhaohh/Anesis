using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

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

            if (IsApproved)
            {
                proposal.ProcedureId = ProcedureId.Value;
                proposal.DiagnosisCode = DiagnosisCode;
                proposal.RequestStatus = PotentialProcedureStatus.Reviewed;
            }
            else
            {
                proposal.RequestStatus = PotentialProcedureStatus.Cancelled;
            }

            proposal.ReviewerNotes = ReviewerNotes;
            proposal.ReviewedDate = currentTime;
            proposal.UpdatedBy = "haotm";
            proposal.UpdatedDate = currentTime;
        }

        public string GetModifiedFields(PotentialProcedure proposal)
        {
            var fields = new List<string>()
            {
                nameof(proposal.RequestStatus),
                nameof(proposal.ReviewedDate),
                nameof(proposal.ReviewerNotes),
            };

            if (IsApproved)
            {
                if (ProcedureId != proposal.ProcedureId)
                    fields.Add(nameof(proposal.ProcedureId));
                if (DiagnosisCode != proposal.DiagnosisCode)
                    fields.Add(nameof(proposal.DiagnosisCode));
            }

            return fields.StrJoin(",");
        }
    }
}
