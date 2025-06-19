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

        public async Task<List<DeviceWithCostDto>> GetAllAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default)
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

        public async Task<bool> UpdateFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default)
        {
            var device = await FindByIdAsync(model.Id, cancellationToken);

            switch (model.FieldName)
            {
                case nameof(DeviceAndSupply.DisplayOrder):
                    device.DisplayOrder = model.NewValue.ToInt();
                    break;
                default:
                    return false;
            }

            device.UpdatedBy = "haotm";
            device.UpdatedDate = DateTime.Now;

            _deviceRepo.Update(device);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> ToggleFlagAsync(FlagToggleDto model, CancellationToken cancellationToken = default)
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

        public async Task<bool> UpdateCostAsync(DeviceCostEditDto model, CancellationToken cancellationToken = default)
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

        public async Task<bool> CreateAsync(DeviceWithCostEditDto model, CancellationToken cancellationToken = default)
        {
            // Insert the new device
            var newDevice = model.ToDeviceModel();
            await _deviceRepo.InsertAsync(newDevice, cancellationToken);

            // Insert the cost for the new device
            var newDeviceCost = model.ToDeviceCostModel(newDevice);
            await _deviceCostRepo.InsertAsync(newDeviceCost, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> UpdateAsync(DeviceWithCostEditDto model, CancellationToken cancellationToken = default)
        {
            // Find the existing device by ID
            var existingDevice =  await FindByIdAsync(model.Id, cancellationToken);
            if (existingDevice == null)
            {
                return false;
            }

            // Apply changes to the device entity
            model.ApplyChangesTo(existingDevice);
            _deviceRepo.Update(existingDevice);

            // Find the current cost for the device
            var currentDeviceCost = await FindCurrentCostAsync(existingDevice.Id);

            // If there is no current cost, insert a new one
            if (currentDeviceCost == null)
            {
                var newDeviceCost = model.ToDeviceCostModel(existingDevice.Id);
                await _deviceCostRepo.InsertAsync(newDeviceCost, cancellationToken);

                return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }

            // If the effective date matches or is overwritten, apply changes to the cost
            if (currentDeviceCost.EffectiveDate.Date == model.EffectiveDate.Value.Date || model.OverwriteEffDate)
            {
                model.ApplyCostChanges(currentDeviceCost);
            }
            // If the effective date does not match, set the end date and insert a new cost
            else
            {
                // Set the end date for the current cost
                currentDeviceCost.EndDate = model.EffectiveDate.Value.Date.AddDays(-1);
                currentDeviceCost.UpdatedBy = "haotm";
                currentDeviceCost.UpdatedDate = DateTime.Now;

                // Insert a new cost for the device with the new effective date
                var newDeviceCost = model.ToDeviceCostModel(existingDevice.Id);
                await _deviceCostRepo.InsertAsync(newDeviceCost, cancellationToken);
            }

            _deviceCostRepo.Update(currentDeviceCost);

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
