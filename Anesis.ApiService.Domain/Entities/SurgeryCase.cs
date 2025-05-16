using Anesis.Shared.Constants;
using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class SurgeryCase : IEntity
    {
        public int Id { get; set; }

        public int EncounterId { get; set; }

        public int PatientId { get; set; }

        public int PrimaryProviderId { get; set; }

        public int? AttendingProviderId { get; set; }

        public int LocationId { get; set; }

        public int EncounterTypeId { get; set; }

        public int ProcedureId { get; set; }

        public int InsuranceId { get; set; }

        public int? ReferringProviderId { get; set; }

        public int WeekId { get; set; }

        public DateTime SurgeryDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan? AnesthesiaStartTime { get; set; }

        public TimeSpan? AnesthesiaEndTime { get; set; }

        public string RoomName { get; set; }

        public string SurgeryReason { get; set; }

        public string IcdCodes { get; set; }

        public SurgeryCaseStatus CaseStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public int? PurchaseInvoiceId { get; set; }

        public decimal InvoiceAmount { get; set; }


        public Patient Patient { get; set; }

        public Provider PrimaryProvider { get; set; }

        public Provider AttendingProvider { get; set; }

        public Location Location { get; set; }

        public EncounterType EncounterType { get; set; }

        public Procedure Procedure { get; set; }

        public Insurance Insurance { get; set; }

        public ReferringProvider ReferringProvider { get; set; }

        public Week Week { get; set; }

        public GenericInvoice PurchaseInvoice { get; set; }

        public List<SurgeryCaseCost> DeviceCosts { get; set; }

        public List<SurgeryCaseCptCode> CptCodes { get; set; }
    }
}
