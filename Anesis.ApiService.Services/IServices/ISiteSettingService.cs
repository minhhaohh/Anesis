using Anesis.ApiService.Domain.DTOs.SiteSettings;

namespace Anesis.ApiService.Services.IServices
{
    public interface ISiteSettingService
    {
        Task<T> LoadSettingsAsync<T>(CancellationToken cancellationToken = default) where T : ISettings, new();

        Task<ISettings> LoadSettingsAsync(Type settingType, CancellationToken cancellationToken = default);
    }
}
