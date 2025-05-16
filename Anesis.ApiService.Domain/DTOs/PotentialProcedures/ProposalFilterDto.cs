using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.PotentialProcedures
{
    public class ProposalFilterDto : PageableSearchModelBase
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

        public PotentialProcedureStatus? RequestStatus { get; set; }

        public bool ExcludeCancelled { get; set; }
    }
}
