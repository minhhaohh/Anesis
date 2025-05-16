using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalDto
    {
        public int Id { get; set; }

        public int ProposerId { get; set; }

        public string ProposerName { get; set; }

        public int PatientId { get; set; }

        public string ChartNo { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int ProviderId { get; set; }

        public string ProviderName { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public bool ChartNotePosted { get; set; }

        public string DiagnosisCode { get; set; }

        public PotentialProcedureStatus RequestStatus { get; set; }

        public string RequestStatusStr => RequestStatus.ToString();

        public DateTime? ReviewedDate { get; set; }

        public int? ReviewerId { get; set; }

        public string ReviewerName { get; set; }

        public string ReviewerNotes { get; set; }

        public bool IsReviewer => ReviewedDate.HasValue;

        public string ScheduledBy { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public TimeSpan? SurgeryTime { get; set; }

        public string SurgeryTimeStr => SurgeryTime.ToTimeString() ?? "";

        public int? SurgeonId { get; set; }

        public string SurgeonName { get; set; }

        public int? SurgeryLocationId { get; set; }

        public string SurgeryLocationName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }

        public string NextStep
            => RequestStatus == PotentialProcedureStatus.Pending
            ? "Review"
            : RequestStatus == PotentialProcedureStatus.Reviewed
                ? "Mark as ordered"
                : RequestStatus == PotentialProcedureStatus.Ordered
                    ? "Schedule surgery"
                    : "Mark as completed";

        public bool CanEdit => RequestStatus == PotentialProcedureStatus.Pending || RequestStatus == PotentialProcedureStatus.Reviewed;

        public bool CanCancel => RequestStatus != PotentialProcedureStatus.Cancelled && RequestStatus != PotentialProcedureStatus.Completed;

        public bool CanReview => RequestStatus == PotentialProcedureStatus.Pending;

        public bool CanMarkAsOrdered => RequestStatus == PotentialProcedureStatus.Reviewed;

        public bool CanSchedule => RequestStatus == PotentialProcedureStatus.Ordered;

        public bool CanMarkAsCompleted => RequestStatus == PotentialProcedureStatus.Scheduled;

    }
}
