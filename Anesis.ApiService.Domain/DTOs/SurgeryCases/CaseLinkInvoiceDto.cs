namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseLinkInvoiceDto
    {
        public int CaseId { get; set; }

        public int InvoiceId { get; set; }

        public decimal SeparateAmount { get; set; }
    }
}
