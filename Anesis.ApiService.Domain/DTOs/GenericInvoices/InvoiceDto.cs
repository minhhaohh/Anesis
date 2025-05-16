using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;

namespace Anesis.ApiService.Domain.DTOs.GenericInvoices
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorDescription { get; set; }

        public string PurchaseOrderNo { get; set; }

        public string Company { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public InvoiceFileType FileType { get; set; }

        public string FileTypeName { get; set; }

        public string ScannedFileName { get; set; }

        public string ScannedFileShortName
            => ScannedFileName.HasValue() ? ScannedFileName.Substring(ScannedFileName.LastIndexOf("/") + 1) : "";

        public decimal Subtotal { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal AdditionalCharges { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime DueDate { get; set; }

        public decimal AmountToPay { get; set; }

        public string FeeNotes { get; set; }

        public string UserComment { get; set; }

        public InvoiceCategory CategoryId { get; set; }

        public string CategoryName => CategoryId.ToString();

        public InvoiceStatus InvoiceStatus { get; set; }

        public string InvoiceStatusStr => InvoiceStatus.ToString();

        public DateTime? PaymentDate { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public string PaymentMethodName => PaymentMethod?.ToString();

        public string PaymentNotes { get; set; }

        public string TransactionId { get; set; }

        public decimal? AmountPaid { get; set; }

        public string PaidBy { get; set; }

        public string ReviewerId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public bool IsBulk { get; set; }

        public string NextStep
            => InvoiceStatus == InvoiceStatus.Pending
            ? "Waiting on approval"
            : InvoiceStatus == InvoiceStatus.Dispute
                ? "Waiting on resolve or cancel"
                : InvoiceStatus == InvoiceStatus.Approved
                    ? "Waiting on payment"
                    : "";

        public string NextActionBy
            => InvoiceStatus == InvoiceStatus.Pending || InvoiceStatus == InvoiceStatus.Approved
            ? ReviewerId
            : InvoiceStatus == InvoiceStatus.Dispute
                ? (CreatedBy != ReviewerId ? $"{CreatedBy} or {ReviewerId}" : CreatedBy)
                : "";
        public bool CanEdit => InvoiceStatus < InvoiceStatus.Approved;

        public bool CanEditBasicInfo => true;

        public bool CanPay => InvoiceStatus == InvoiceStatus.Approved;

        public bool CanDelete => true;

        public int ItemsCount { get; set; }

        public List<InvoiceItemDto> Items { get; set; }
    }
}
