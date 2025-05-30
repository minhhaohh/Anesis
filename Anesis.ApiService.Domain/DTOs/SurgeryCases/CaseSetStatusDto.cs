using Anesis.ApiService.Domain.Entities;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseSetStatusDto
    {
        public int Id { get; set; }

        public int? Status { get; set; }

        public string Reason { get; set; }

        public void ApplyChangesTo(SurgeryCase surgeryCase)
        {
            surgeryCase.CaseStatus = (SurgeryCaseStatus)Status;
            surgeryCase.UpdatedBy = "haotm";
            surgeryCase.UpdatedDate = DateTime.Now;

            if (Reason.HasValue())
            {
                surgeryCase.Notes = $"{surgeryCase.Notes} | {(SurgeryCaseStatus)Status} Reason: {Reason}";
            }
        }
    }
}
