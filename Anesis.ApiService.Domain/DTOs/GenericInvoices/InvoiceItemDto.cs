namespace Anesis.ApiService.Domain.DTOs.GenericInvoices
{
    public class InvoiceItemDto
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
    }
}
