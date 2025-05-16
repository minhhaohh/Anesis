using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Providers;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Provider> _providerRepo;

        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _providerRepo = _unitOfWork.GetRepository<Provider>();
        }

        public async Task<IPagedList<Provider>> SearchAsync(
            ProviderFilterDto filter, CancellationToken cancellationToken = default)
        {
            return await _providerRepo.All(true)
                .WhereIf(x => x.IsActive, filter.ActiveOnly)
                .SortData(filter.Sidx, filter.Sord, nameof(Provider.ProviderName))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _providerRepo.All(true)
                .Where(x => x.Id > 0)                // Ignore dummy providers
                .WhereIf(x => x.IsActive, activeOnly)
                .OrderBy(x => x.ProviderName)
                .ToDictionaryAsync(x => x.Id, x => x.ProviderName, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetDoctorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _providerRepo.All(true)
                .Where(x => x.Id > 0)                // Ignore dummy providers
                .Where(x => ProviderTypes.GetDoctor().Contains(x.ProviderType))
                .WhereIf(x => x.IsActive, activeOnly)
                .OrderBy(x => x.ProviderName)
                .ToDictionaryAsync(x => x.Id, x => x.ProviderName, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetMidLevelProvidersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _providerRepo.All(true)
                .Where(x => x.Id > 0)                // Ignore dummy providers
                .Where(x => ProviderTypes.GetMidLevel().Contains(x.ProviderType))
                .WhereIf(x => x.IsActive, activeOnly)
                .OrderBy(x => x.ProviderName)
                .ToDictionaryAsync(x => x.Id, x => x.ProviderName, cancellationToken);
        }
    }
}
