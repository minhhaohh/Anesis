using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalScheduleDto
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
        }
    }
}
