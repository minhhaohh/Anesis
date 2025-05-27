using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class SurgeryCaseFilterModel : PageableSearchModelBase
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? SurgeryDate { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ProcedureId { get; set; }

        public int? LocationId { get; set; }

        public int? PrimaryProviderId { get; set; }

        public int? InsuranceId { get; set; }

        public int? CaseStatus { get; set; }

        public bool HasDevices { get; set; }
        
        public bool NoDevices { get; set; }

        public bool LinkedInvoice { get; set; }

        public bool NotLinkInvoice { get; set; }

        public string VendorName { get; set; }

        public string LinkedInvoiceNo { get; set; }

        public int? PurchaseInvoiceId { get; set; }

        public bool SelectedOnly { get; set; }

        public string SelectedCaseIds { get; set; }
    }

    public class SurgeryCaseViewModel
    {
        public int Id { get; set; }

        public int EncounterId { get; set; }

        public int PatientId { get; set; }

        public string ChartNo { get; set; }

        public string EcwChartNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

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

        public int WeekNumber { get; set; }

        public DateTime SurgeryDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan? AnesthesiaStartTime { get; set; }

        public TimeSpan? AnesthesiaEndTime { get; set; }

        public string RoomName { get; set; }

        public string SurgeryReason { get; set; }

        public string IcdCodes { get; set; }

        public int CaseStatus { get; set; }

        public string CaseStatusStr { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public int? PurchaseInvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal InvoiceAmount { get; set; }

        public bool IsBulk { get; set; }

        public bool CanEditCases { get; set; }

        public bool CanManageCptCodes { get; set; }

        public bool CanManageDevices { get; set; }

        public bool CanLinkInvoice { get; set; }


        public List<string> Devices { get; set; }

        public List<string> Codes { get; set; }
    }

    public class LinkInvoiceCaseModel
    {
        public int CaseId { get; set; }

        public int? InvoiceId { get; set; }

        public decimal SeparateAmount { get; set; }
    }
    
    public class CaseCostFilterModel : PageableSearchModelBase
    {
        public int CaseId { get; set; }
    }

    public class CaseCostViewModel
    {
        public int Id { get; set; }

        public int SurgeryCaseId { get; set; }

        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string VendorName { get; set; }

        public int CostId { get; set; }

        public int Quantity { get; set; }

        public decimal VendorCost { get; set; }

        public decimal AppliedCost { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }
    }

    public class CaseCostAddModel
    {
        public int CaseId { get; set; }

        public int? DeviceId { get; set; }

        public int? Quantity { get; set; }

        public string Notes { get; set; }

        public CaseCostAddModel()
        {
            Quantity = 1;
        }
    }

    public class CaseCptCodeFilterModel : PageableSearchModelBase
    {
        public int CaseId { get; set; }
    }

    public class CaseCptCodeViewModel
    {
        public int Id { get; set; }

        public int SurgeryCaseId { get; set; }

        public int BillingCodeId { get; set; }

        public string CptCode { get; set; }

        public int RvuConfigId { get; set; }

        public DateTime RvuEffectiveDate { get; set; }

        public decimal WorkRvu { get; set; }

        public decimal ConversionFactor { get; set; }

        public int Quantity { get; set; }

        public int AllowedAmountId { get; set; }

        public decimal CmsAllowedAmt { get; set; }

        public decimal AllowedAmountPct { get; set; }

        public decimal? CollectedAmount { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public decimal ContractedPct { get; set; }

        public decimal AllowedAmtValue { get; set; }
    }

    public class CaseCptCodeAddModel
    {
        public int CaseId { get; set; }

        public int? BillingCodeId { get; set; }

        public string CptCode { get; set; }

        public int? Quantity { get; set; }

        public string Notes { get; set; }

        public CaseCptCodeAddModel()
        {
            Quantity = 1;
        }
    }
}
