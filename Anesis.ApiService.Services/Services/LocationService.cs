using Anesis.ApiService.Domain.Constants;
using Anesis.Shared.Models;
using Anesis.ApiService.Domain.DTOs.Locations;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Location> _locationRepo;

        public LocationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _locationRepo = _unitOfWork.GetRepository<Location>();
        }

        public async Task<IPagedList<LocationDto>> SearchAsync(LocationFilterDto filter, 
            CancellationToken cancellationToken = default)
        {
            return await _mapper.ProjectTo<LocationDto>(Search(filter))
                .SortData(filter.Sidx, filter.Sord, nameof(Location.ShortName))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<LocationDto> GetByIdAsync(int id, 
            CancellationToken cancellationToken = default)
        {
            return _mapper.Map<LocationDto>(await FindByIdAsync(id, cancellationToken));
        }

        public async Task<Dictionary<int, string>> GetLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _locationRepo.All(true)
                .Where(x => x.LocationType == LocationType.CLINIC)
                .WhereIf(x => x.IsActive, activeOnly)
                .OrderBy(x => x.ShortName)
                .ToDictionaryAsync(x => x.Id, x => x.ShortName, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetAscLocationsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _locationRepo.All(true)
                .Where(x => x.LocationType == LocationType.CLINIC && x.ShortName.Contains("ASC"))
                .WhereIf(x => x.IsActive, activeOnly)
                .OrderBy(x => x.ShortName)
                .ToDictionaryAsync(x => x.Id, x => x.ShortName, cancellationToken);
        }

        // SUPPORT METHODS
        private IQueryable<Location> Search(LocationFilterDto filter)
        {
            return _locationRepo.All(true)
                .WhereIf(x => x.LocationType == filter.LocationType, filter.LocationType.HasValue())
                .WhereIf(x => x.EcwSiteId > 0, filter.HasSiteId)
                .WhereIf(x => x.IsActive, filter.ActiveOnly);
        }

        private async Task<Location> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _locationRepo.FindAsync(cancellationToken, id);
        }
    }
}
