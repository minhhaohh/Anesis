using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class DeviceService : IDeviceService
    {
		private readonly IMapper _mapper; 
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<DeviceAndSupply> _deviceRepo;
		private readonly IRepository<DeviceCost> _deviceCostRepo;

		public DeviceService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_deviceRepo = _unitOfWork.GetRepository<DeviceAndSupply>();
			_deviceCostRepo = _unitOfWork.GetRepository<DeviceCost>();
		}

		public async Task<IPagedList<DeviceWithCostDto>> SearchAsync(
            DeviceFilterDto filter, CancellationToken cancellationToken = default)
		{
			var query = Search(filter);

            return await query
				.Join(
					_deviceCostRepo.All(true).Where(x => x.EndDate == null),
					device => device.Id,
					cost => cost.DeviceId,
					(d, c) => new DeviceWithCostDto()
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        VendorName = d.VendorName,
                        DeviceCode = d.DeviceCode,
                        Category = d.Category,
                        IsActive = d.IsActive,
                        DisplayOrder = d.DisplayOrder,
                        VendorCost = c.VendorCost,
                        AppliedCost = c.AppliedCost,
                        EffectiveDate = c.EffectiveDate,
                        EndDate = c.EndDate,
                        UpdatedDate = d.UpdatedDate,
                        UpdatedBy = d.UpdatedBy,
                        Notes = d.Notes,
                    })
                .SortData(filter.Sidx, filter.Sord, nameof(DeviceWithCostDto.Name))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
		}

        public async Task<List<DeviceWithCostDto>> GetAllAsync(bool activeOnly = false,
            CancellationToken cancellationToken = default)
        {
            var query = _deviceRepo.All(true)
                .WhereIf(x => x.IsActive, activeOnly);

            return await query
                .Join(
                    _deviceCostRepo.All(true).Where(x => x.EndDate == null),
                    device => device.Id,
                    cost => cost.DeviceId,
                    (d, c) => new DeviceWithCostDto()
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        VendorName = d.VendorName,
                        DeviceCode = d.DeviceCode,
                        Category = d.Category,
                        IsActive = d.IsActive,
                        DisplayOrder = d.DisplayOrder,
                        VendorCost = c.VendorCost,
                        AppliedCost = c.AppliedCost,
                        EffectiveDate = c.EffectiveDate,
                        EndDate = c.EndDate,
                        UpdatedDate = d.UpdatedDate,
                        UpdatedBy = d.UpdatedBy,
                        Notes = d.Notes,
                    })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.VendorName)
                .ToListAsync(cancellationToken);
        }

        public async Task<DeviceWithCostDto> GetByIdAsync(
			int id, CancellationToken cancellationToken = default)
        {
            var deviceWithCost = _mapper.Map<DeviceWithCostDto>(await FindByIdAsync(id, cancellationToken));

            if (deviceWithCost == null)
            {
                return null;
            }

			var currentCost = await FindCurrentCostAsync(id, cancellationToken);

            deviceWithCost.VendorCost = currentCost.VendorCost;
            deviceWithCost.AppliedCost = currentCost.AppliedCost;
            deviceWithCost.EffectiveDate = currentCost.EffectiveDate;

			return deviceWithCost;
        }

        public async Task<DeviceCostDto> GetCurrentCostAsync(
			int deviceId, CancellationToken cancellationToken = default)
        {
			var currentCost = await FindCurrentCostAsync(deviceId, cancellationToken);
            return _mapper.Map<DeviceCostDto>(currentCost);
        }

        public async Task<List<DeviceCostDto>> GetPriceHistoryAsync(
			int deviceId, CancellationToken cancellationToken = default)
        {
            var query = _deviceCostRepo.All(true)
                .Where(x => x.DeviceId == deviceId);

            return await _mapper.ProjectTo<DeviceCostDto>(query)
                .OrderByDescending(x => x.EffectiveDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ToggleFlagAsync(
			FlagToggleDto model, CancellationToken cancellationToken = default)
		{
            var device = await FindByIdAsync(model.Id, cancellationToken);

            switch (model.FieldName)
            {
                case nameof(DeviceAndSupply.IsActive):
                    device.IsActive = !device.IsActive;
                    break;
                default:
                    return false;
            }

            device.UpdatedBy = "haotm";
            device.UpdatedDate = DateTime.Now;

            _deviceRepo.Update(device);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateDeviceCostAsync(
            DeviceCostEditDto model, CancellationToken cancellationToken = default)
		{
            var deviceCost = await FindCurrentCostAsync(model.DeviceId);

            if (deviceCost == null)
            {
                await _deviceCostRepo.InsertAsync(model.ToDeviceCost(), cancellationToken);
                return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }

            if (deviceCost.EffectiveDate.Date == model.EffectiveDate.Value.Date || model.OverwriteEffDate)
            {
                model.ApplyChangesTo(deviceCost);
            }
            else
            {
                deviceCost.EndDate = model.EffectiveDate.Value.Date.AddDays(-1);
                deviceCost.UpdatedBy = "haotm";
                deviceCost.UpdatedDate = DateTime.Now;

                await _deviceCostRepo.InsertAsync(model.ToDeviceCost(), cancellationToken);
            }

            _deviceCostRepo.Update(deviceCost);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> CreateDeviceAsync(
            DeviceWithCostEditDto model, CancellationToken cancellationToken = default)
        {
            var device = _mapper.Map<DeviceAndSupply>(model);

            device.CreatedDate = device.UpdatedDate = DateTime.Now;
            device.CreatedBy = device.UpdatedBy = "haotm";

            await _deviceRepo.InsertAsync(device, cancellationToken);

            await _deviceCostRepo.InsertAsync(model.ToDeviceCost(device), cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateDeviceAsync(
            DeviceWithCostEditDto model, CancellationToken cancellationToken = default)
        {
            var currentTime = DateTime.Now;
            var device =  await FindByIdAsync(model.Id, cancellationToken);

            _mapper.Map(model, device); 

            device.UpdatedDate = currentTime;
            device.UpdatedBy = "haotm";

            _deviceRepo.Update(device);

            var deviceCost = await FindCurrentCostAsync(device.Id);

            if (deviceCost == null)
            {
                await _deviceCostRepo.InsertAsync(model.ToDeviceCost(device.Id), cancellationToken);
                return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }

            if (deviceCost.EffectiveDate.Date == model.EffectiveDate.Value.Date || model.OverwriteEffDate)
            {
                model.ApplyCostChanges(deviceCost);
            }
            else
            {
                deviceCost.EndDate = model.EffectiveDate.Value.Date.AddDays(-1);
                deviceCost.UpdatedBy = "haotm";
                deviceCost.UpdatedDate = currentTime;

                await _deviceCostRepo.InsertAsync(model.ToDeviceCost(device.Id), cancellationToken);
            }

            _deviceCostRepo.Update(deviceCost);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        // SUPPORT METHODS
        public IQueryable<DeviceAndSupply> Search(DeviceFilterDto filter)
        {
            return _deviceRepo.All(true)
                .WhereIf(x => x.Name.Contains(filter.Name), filter.Name.HasValue())
                .WhereIf(x => x.VendorName.Contains(filter.VendorName), filter.VendorName.HasValue())
                .WhereIf(x => (int)x.Category == filter.Category, filter.Category != null)
                .WhereIf(x => x.IsActive, filter.ActiveOnly);
        }

        public async Task<DeviceAndSupply> FindByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _deviceRepo.FindAsync(cancellationToken, id);
        }

        private async Task<DeviceCost> FindCurrentCostAsync(
           int id, CancellationToken cancellationToken = default)
        {
            return await _deviceCostRepo.GetFirstAsync(
                x => x.DeviceId == id && x.EndDate == null, false, cancellationToken);
        }
    }
}
