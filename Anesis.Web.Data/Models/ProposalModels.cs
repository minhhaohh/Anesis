using Anesis.Shared.Constants;
using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class ProposalFilterModel : PageableSearchModelBase
    {
        public int? ProposerId { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public int? ProviderId { get; set; }

        public int? LocationId { get; set; }

        public int? ProcedureId { get; set; }

        public int? ReviewerId { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public int? SurgeonId { get; set; }

        public int? SurgeryLocationId { get; set; }

        public bool UnpostedChartNote { get; set; }

        public int? RequestStatus { get; set; }

        public bool ExcludeCancelled { get; set; }

        public ProposalFilterModel()
        {
            ExcludeCancelled = true;
        }
    }

    public class ProposalViewModel
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

        public int RequestStatus { get; set; }

        public string RequestStatusStr { get; set; }

        public DateTime? ReviewedDate { get; set; }

        public int? ReviewerId { get; set; }

        public string ReviewerName { get; set; }

        public string ReviewStatus { get; set; }

        public string ReviewerNotes { get; set; }

        public string ScheduledBy { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public TimeSpan? SurgeryTime { get; set; }

        public int? SurgeonId { get; set; }

        public string SurgeonName { get; set; }

        public int? SurgeryLocationId { get; set; }

        public string SurgeryLocationName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }

        public ProposalEditModel ToEditModel()
        {
            return new ProposalEditModel()
            {
                Id = Id,
                PatientId = PatientId,
                EcwChartNo = EcwChartNo,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                Dob = Dob,
                AppointmentDate = AppointmentDate,
                ProviderId = ProviderId,
                LocationId = LocationId,
                ProcedureId = ProcedureId,
                ChartNotePosted = ChartNotePosted,
                DiagnosisCode = DiagnosisCode,
                ReviewerId = ReviewerId,
                Notes = Notes
            };
        }
    }

    public class ProposalEditModel
    {
        public int? Id { get; set; }

        public int? PatientId { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public int? ProviderId { get; set; }

        public int? LocationId { get; set; }

        public int? ProcedureId { get; set; }

        public bool ChartNotePosted { get; set; }

        public string DiagnosisCode { get; set; }

        public int? ReviewerId { get; set; }

        public string Notes { get; set; }

        public string ReasonChange { get; set; }
    }

    public class ProposalReviewModel
    {
        public int Id { get; set; }

        public int? ProcedureId { get; set; }

        public string DiagnosisCode { get; set; }

        public string ReviewerNotes { get; set; }

        public bool IsApproved { get; set; }
    }

    public class ProposalScheduleSurgeryModel
    {
        public int Id { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public TimeSpan? SurgeryTime { get; set; }

        public int? SurgeonId { get; set; }

        public int? SurgeryLocationId { get; set; }

        public string Notes { get; set; }
    }

    public class ProposalSetStatusModel
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public string Reason { get; set; }
    }
}
