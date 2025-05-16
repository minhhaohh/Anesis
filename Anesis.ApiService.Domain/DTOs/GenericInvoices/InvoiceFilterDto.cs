using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.DTOs.GenericInvoices
{
    public class InvoiceFilterDto : PageableSearchModelBase
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string Company { get; set; }

        public int? LocationId { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public InvoiceStatus? InvoiceStatus { get; set; }

        public string ReviewerId { get; set; }

        public string CreatedBy { get; set; }

        public bool BulkInvoicesOnly { get; set; }

        public bool UnpaidInvoicesOnly { get; set; }
    }
}
