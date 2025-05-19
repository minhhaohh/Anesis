using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalEditDto
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public int? ProviderId { get; set; }

        public int? LocationId { get; set; }

        public int? ProcedureId { get; set; }

        public bool ChartNotePosted { get; set; }

        public string DiagnosisCode { get; set; }

        public int? ReviewerId { get; set; }

        public string Notes { get; set; }

        public string ReasonChange { get; set; }

        public PotentialProcedure ToPotentialProcedureModel()
        {
            return new PotentialProcedure()
            {
                ProposerId = 1,
                PatientId = PatientId.Value,
                AppointmentDate = AppointmentDate,
                ProviderId = ProviderId.Value,
                LocationId = LocationId.Value,
                ProcedureId = ProcedureId.Value,
                ChartNotePosted = ChartNotePosted,
                DiagnosisCode = DiagnosisCode,
                ReviewerId = ReviewerId,
                CreatedBy = "haotm",
                CreatedDate = DateTime.Now,
                UpdatedBy = "haotm",
                UpdatedDate = DateTime.Now,
                Notes = Notes,
            };
        }

        public void ApplyChangesTo(PotentialProcedure proposal)
        {
            if ((ProcedureId != proposal.ProcedureId || ReviewerId != proposal.ReviewerId)
                && proposal.RequestStatus == PotentialProcedureStatus.Reviewed)
            {
                proposal.RequestStatus = PotentialProcedureStatus.Pending;
                proposal.ReviewedDate = null;
                proposal.ReviewerNotes = null;
            }
            proposal.AppointmentDate = AppointmentDate;
            proposal.ProviderId = ProviderId.Value;
            proposal.LocationId = LocationId.Value;
            proposal.ProcedureId = ProcedureId.Value;
            proposal.ChartNotePosted = ChartNotePosted;
            proposal.DiagnosisCode = DiagnosisCode;
            proposal.ReviewerId = ReviewerId;
            proposal.UpdatedDate = DateTime.Now;
            proposal.UpdatedBy = "haotm";
            proposal.Notes = Notes;
        }

        public string GetModifiedFields(PotentialProcedure proposal)
        {
            var fields = new List<string>();

            if (PatientId != proposal.PatientId)
                fields.Add(nameof(proposal.PatientId));
            if (AppointmentDate != proposal.AppointmentDate)
                fields.Add(nameof(proposal.AppointmentDate));
            if (ProviderId != proposal.ProviderId)
                fields.Add(nameof(proposal.ProviderId));
            if (LocationId != proposal.LocationId)
                fields.Add(nameof(proposal.LocationId));
            if (ProcedureId != proposal.ProcedureId)
                fields.Add(nameof(proposal.ProcedureId));
            if (ChartNotePosted != proposal.ChartNotePosted)
                fields.Add(nameof(proposal.ChartNotePosted));
            if (DiagnosisCode != proposal.DiagnosisCode)
                fields.Add(nameof(proposal.DiagnosisCode));
            if (ReviewerId != proposal.ReviewerId)
                fields.Add(nameof(proposal.ReviewerId));
            if (Notes != proposal.Notes)
                fields.Add(nameof(proposal.Notes));

            return fields.StrJoin(",");
        }
    }
}
