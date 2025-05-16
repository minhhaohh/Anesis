using Anesis.ApiService.Domain.DTOs.SiteSettings;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class SiteSettingService : ISiteSettingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SiteSetting> _siteSettingRepo;

        public SiteSettingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _siteSettingRepo = unitOfWork.GetRepository<SiteSetting>();
        }   

        public async Task<T> LoadSettingsAsync<T>(CancellationToken cancellationToken = default) where T : ISettings, new()
        {
            return (T)(await LoadSettingsAsync(typeof(T), cancellationToken));
        }

        public async Task<ISettings> LoadSettingsAsync(Type settingType, CancellationToken cancellationToken = default)
        {
            var rawSettings = await GetRawSettingsAsync(settingType, true, cancellationToken);
            return MaterializeSettings(settingType, rawSettings);
        }

        // SUPPORT METHODS
        private async Task<IDictionary<string, SiteSetting>> GetRawSettingsAsync(
			Type settingType, bool disableTracking = false, CancellationToken cancellationToken = default)
        {
            var prefix = settingType.Name;
            var settings = await _siteSettingRepo
                .All(disableTracking)
                .Where(x => x.SettingName.StartsWith(prefix))
                .ToListAsync(cancellationToken);

            return settings.ToDictionary(x => x.SettingName, x => x, StringComparer.OrdinalIgnoreCase);
        }

        private ISettings MaterializeSettings(Type settingsType, IDictionary<string, SiteSetting> rawSettings)
		{
			var instance = Activator.CreateInstance(settingsType);
			var prefix = settingsType.Name;

			foreach (var prop in settingsType.GetProperties())
			{
				if (!prop.CanWrite)
                {
                    continue;
                }

				var key = prefix + "." + prop.Name;
				rawSettings.TryGetValue(key, out var setting);

				var settingValue = setting?.SettingValue;
				if (settingValue == null)
				{
                    continue;
                }

				var convertedValue = Convert.ChangeType(settingValue, prop.PropertyType);
                prop.SetValue(instance, convertedValue);
			}

			return instance as ISettings;
		}
    }
}
