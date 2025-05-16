using Anesis.ApiService.Domain.Entities.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Domain.Entities
{
    public class GenericInvoice : IEntity
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public string Company { get; set; }

        public int LocationId { get; set; }

        public InvoiceFileType FileType { get; set; }

        public string ScannedFileName { get; set; }

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

        public InvoiceStatus InvoiceStatus { get; set; }

        public DateTime? PaymentDate { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

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


        public Customer Vendor { get; set; }

        public Location Location { get; set; }

        public List<GenericInvoiceItem> Items { get; set; }
    }
}
