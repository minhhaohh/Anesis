using Anesis.Shared.Constants;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class SurgeryCaseFilterDto : PageableSearchModelBase
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

        public SurgeryCaseStatus? CaseStatus { get; set; }

        public bool HasDevices { get; set; }

        public bool NoDevices { get; set; }

        public bool LinkedInvoice { get; set; }

        public bool NotLinkInvoice { get; set; }

        public string VendorName { get; set; }

        public string LinkedInvoiceNo { get; set; }

        public int? PurchaseInvoiceId { get; set; }

        public bool SelectedOnly { get; set; }

        public string SelectedCaseIds { get; set; }

        public List<int>  CaseIds => SelectedCaseIds.HasValue()
                ? SelectedCaseIds.Split(",").Select(int.Parse).ToList()
                : new List<int>();
    }
}
