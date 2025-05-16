namespace Anesis.ApiService.Domain.DTOs.SiteSettings
{
    public class SiteSettingDto
    {
        public int Id { get; set; }

        public string SettingName { get; set; }

        public string SettingValue { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Notes { get; set; }
    }
}
