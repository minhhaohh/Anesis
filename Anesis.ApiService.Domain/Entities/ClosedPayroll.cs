using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class ClosedPayroll : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ClosedDate { get; set; }

        public string AccountId { get; set; }

        public decimal GrossHours { get; set; }

        public decimal DeductedHours { get; set; }

        public decimal UnpaidHours { get; set; }

        public decimal NetHours { get; set; }

        public string Company { get; set; }

        public string Notes { get; set; }

        public Account Account { get; set; }
    }
}
