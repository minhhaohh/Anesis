using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class PreAuthInsuranceGroup : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }
    }
}
