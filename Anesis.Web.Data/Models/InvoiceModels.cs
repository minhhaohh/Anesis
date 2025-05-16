using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class InvoiceFilterModel : PageableSearchModelBase
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string Company { get; set; }

        public int? LocationId { get; set; }

        public int? PaymentMethod { get; set; }

        public int? InvoiceStatus { get; set; }

        public string ReviewerId { get; set; }

        public string CreatedBy { get; set; }

        public bool BulkInvoicesOnly { get; set; }

        public bool UnpaidInvoicesOnly { get; set; }

        public InvoiceFilterModel() 
        {
            UnpaidInvoicesOnly = true;
        }
    }

    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorDescription { get; set; }

        public string PurchaseOrderNo { get; set; }

        public string Company { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int FileType { get; set; }

        public string FileTypeName { get; set; }

        public string ScannedFileName { get; set; }

        public string ScannedFileShortName { get; set; }

        public decimal Subtotal { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal AdditionalCharges { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AmountToPay { get; set; }

        public string FeeNotes { get; set; }

        public string UserComment { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int InvoiceStatus { get; set; }

        public string InvoiceStatusStr { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? PaymentMethod { get; set; }

        public string PaymentMethodName { get; set; }

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

        public string NextStep { get; set; }

        public string NextActionBy { get; set; }

        public bool CanEdit { get; set; }

        public bool CanEditBasicInfo { get; set; }

        public bool CanPay { get; set; }

        public bool CanDelete { get; set; }

        public int ItemsCount { get; set; }

        public List<InvoiceItemViewModel> Items { get; set; }

        public InvoiceEditModel ToEditModel()
        {
            var invoiceItems = Items != null && Items.Count > 0
                ? Items.Select(x => x.ToEditModel()).ToList()
                : new List<InvoiceItemEditModel>();

            while (invoiceItems.Count < 20) 
            {
                invoiceItems.Add(new InvoiceItemEditModel());
            }

            return new InvoiceEditModel()
            {
                Id = Id,
                InvoiceDate = InvoiceDate,
                DueDate = DueDate,
                InvoiceNo = InvoiceNo,
                VendorId = VendorId,
                PurchaseOrderNo = PurchaseOrderNo,
                Company = Company,
                LocationId = LocationId,
                FileType = FileType,
                ScannedFileName = ScannedFileName,
                ScannedFileShortName = ScannedFileShortName,
                Subtotal = Subtotal,
                TaxAmount = TaxAmount,
                ShippingFee = ShippingFee,
                AdditionalCharges = AdditionalCharges,
                DiscountAmount = DiscountAmount,
                TotalAmount = TotalAmount,
                AmountToPay = AmountToPay,
                FeeNotes = FeeNotes,
                UserComment = UserComment,
                Notes = Notes,
                ReviewerId = ReviewerId,
                IsBulk = IsBulk,
                Items = invoiceItems
            };
        }

        public BasicInvoiceEditModel ToBasicEditModel()
        {
            return new BasicInvoiceEditModel()
            {
                Id = Id,
                InvoiceDate = InvoiceDate,
                DueDate= DueDate,
                InvoiceNo = InvoiceNo,
                VendorId = VendorId,
                PurchaseOrderNo = PurchaseOrderNo,
                Company = Company,
                LocationId = LocationId,
                UserComment = UserComment,
                Notes = Notes,
                IsBulk = IsBulk,
            };
        }
    }

    public class InvoiceItemViewModel
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public string ItemName { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string Notes { get; set; }

        public InvoiceItemEditModel ToEditModel()
        {
            return new InvoiceItemEditModel()
            {
                Id = Id,
                ItemName = ItemName,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                Notes = Notes
            };
        }
    }

    public class InvoiceEditModel
    {
        public int Id { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public string Company { get; set; }

        public int? LocationId { get; set; }

        public int? FileType { get; set; }

        public string ScannedFileName { get; set; }

        public string ScannedFileShortName { get; set; }

        public decimal Subtotal { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal AdditionalCharges { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal AmountToPay { get; set; }

        public string FeeNotes { get; set; }

        public string UserComment { get; set; }

        public string Notes { get; set; }

        public string ReviewerId { get; set; }

        public bool IsBulk { get; set; }

        public List<InvoiceLinkSurgeryCaseModel> LinkedCases { get; set; }

        public string LinkedCasesStr { get; set; }

        public List<InvoiceItemEditModel> Items { get; set; }

        public InvoiceEditModel()
        {
            LinkedCases = new List<InvoiceLinkSurgeryCaseModel>();

            var invoiceItems = new List<InvoiceItemEditModel>();

            while (invoiceItems.Count < 20)
            {
                invoiceItems.Add(new InvoiceItemEditModel());
            }

            Items = invoiceItems;
        }
    }

    public class InvoiceItemEditModel
    {
        public int? Id { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string Notes { get; set; }
    }

    public class BasicInvoiceEditModel
    {
        public int Id { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string InvoiceNo { get; set; }

        public int? VendorId { get; set; }

        public string PurchaseOrderNo { get; set; }

        public string Company { get; set; }

        public int? LocationId { get; set; }

        public string UserComment { get; set; }

        public string Notes { get; set; }

        public bool IsBulk { get; set; }
    }

    public class InvoiceLinkSurgeryCaseModel
    {
        public int SurgeryCaseId { get; set; }

        public decimal InvoiceAmount { get; set; }
    }
}
