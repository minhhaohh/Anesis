using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class SurgeryCaseDto
    {
        public int Id { get; set; }

        public int EncounterId { get; set; }

        public int PatientId { get; set; }

        public string ChartNo { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public int PrimaryProviderId { get; set; }

        public string PrimaryProviderName { get; set; }

        public int? AttendingProviderId { get; set; }

        public string AttendingProviderName { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int EncounterTypeId { get; set; }

        public string EncounterTypeName { get; set; }

        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public int InsuranceId { get; set; }

        public string InsuranceName { get; set; }

        public int? ReferringProviderId { get; set; }

        public string ReferringProviderName { get; set; }

        public int WeekId { get; set; }

        public int? YearNumber { get; set; }

        public int? WeekNumber { get; set; }

        public DateTime SurgeryDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan? AnesthesiaStartTime { get; set; }

        public TimeSpan? AnesthesiaEndTime { get; set; }

        public string RoomName { get; set; }

        public string SurgeryReason { get; set; }

        public string IcdCodes { get; set; }

        public SurgeryCaseStatus CaseStatus { get; set; }

        public string CaseStatusStr => CaseStatus.ToString();

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public int? PurchaseInvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal InvoiceAmount { get; set; }

        public bool? IsBulkInvoice { get; set; }

        public bool CanEditCases => CaseStatus == SurgeryCaseStatus.Pending;

        public bool CanManageCptCodes => CaseStatus == SurgeryCaseStatus.Pending;

        public bool CanManageDevices => CaseStatus == SurgeryCaseStatus.Pending;

        public bool CanLinkInvoice 
            => CaseStatus == SurgeryCaseStatus.Pending 
                || (Devices != null && Devices.Count > 0 && PurchaseInvoiceId == null);


        public List<string> Devices { get; set; }

        public List<string> Codes { get; set; }
    }
}
