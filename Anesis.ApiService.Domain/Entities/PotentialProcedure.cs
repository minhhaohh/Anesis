using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.Entities
{
    public class PotentialProcedure : IEntity
    {
        public int Id { get; set; }

        public int ProposerId { get; set; }

        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int ProviderId { get; set; }

        public int LocationId { get; set; }

        public int ProcedureId { get; set; }

        public bool ChartNotePosted { get; set; }

        public string DiagnosisCode { get; set; }

        public PotentialProcedureStatus RequestStatus { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public int? ReviewerId { get; set; }

        public string ReviewerNotes { get; set; }

        public int? SurgeryOrderNo { get; set; }

        public string ScheduledBy { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public TimeSpan? SurgeryTime { get; set; }

        public int? SurgeonId { get; set; }

        public int? SurgeryLocationId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }


        public Provider Proposer { get; set; }

        public Provider ApptProvider { get; set; }

        public Provider Reviewer { get; set; }

        public Provider Surgeon { get; set; }

        public Location ApptLocation { get; set; }

        public Location SurgeryLocation { get; set; }

        public Patient Patient { get; set; }

        public Procedure Procedure { get; set; }
    }
}
