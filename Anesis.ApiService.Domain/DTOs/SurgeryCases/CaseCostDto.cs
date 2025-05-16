namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCostDto
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
}
