using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalScheduleSurgeryDto
    {
        public int Id { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public TimeSpan? SurgeryTime { get; set; }

        public int? SurgeonId { get; set; }

        public int? SurgeryLocationId { get; set; }

        public string Notes { get; set; }

        public void ApplyChangesTo(PotentialProcedure proposal)
        {
            proposal.RequestStatus = PotentialProcedureStatus.Scheduled;
            proposal.SurgeonId = SurgeonId;
            proposal.SurgeryLocationId = SurgeryLocationId;
            proposal.SurgeryDate = SurgeryDate;
            proposal.SurgeryTime = SurgeryTime;
            proposal.ScheduledBy = "haotm";
            proposal.UpdatedDate = DateTime.Now;
            proposal.UpdatedBy = "haotm";
            proposal.Notes = Notes.HasValue() ? $"{proposal} | Surgery Notes: {Notes}" : proposal.Notes;
        }

        public string GetModifiedFields(PotentialProcedure proposal, bool isRescheduled)
        {
            var fields = new List<string>();

            if (SurgeryDate != proposal.SurgeryDate)
                fields.Add(nameof(proposal.SurgeryDate));
            if (SurgeryTime != proposal.SurgeryTime)
                fields.Add(nameof(proposal.SurgeryTime));
            if (SurgeonId != proposal.SurgeonId)
                fields.Add(nameof(proposal.SurgeonId));
            if (SurgeryLocationId != proposal.SurgeryLocationId)
                fields.Add(nameof(proposal.SurgeryLocationId));

            if (!isRescheduled)
            {
                fields.Add(nameof(proposal.RequestStatus));
            }

            return fields.StrJoin(",");
        }
    }
}
