using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class SiteSetting : IEntity
    {
        public int Id { get; set; }

        public string SettingName { get; set; }

        public string SettingValue { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }
    }
}
