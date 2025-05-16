using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Domain.DTOs.SurgeryCases
{
    public class CaseCptCodeAddDto
    {
        public int? CaseId { get; set; }

        public int? BillingCodeId { get; set; }

        public int? Quantity { get; set; }

        public string Notes { get; set; }

        public SurgeryCaseCptCode ToSurgeryCaseCptCode()
        {
            return new SurgeryCaseCptCode()
            {
                SurgeryCaseId = CaseId.Value,
                Quantity = Quantity.Value,
                BillingCodeId = BillingCodeId.Value,
                UpdatedBy = "haotm",
                UpdatedDate = DateTime.Now,
                Notes = Notes
            };
        }
    }
}
