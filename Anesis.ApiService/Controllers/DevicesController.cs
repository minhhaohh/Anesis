using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Devices;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.Devices;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
    //[CustomAuthorize]
    public class DevicesController : ControllerBase
	{
		private readonly ILogger<DevicesController> _logger;
		private readonly IDeviceService _deviceService;

		public DevicesController(
			ILogger<DevicesController> logger,
            IDeviceService deviceService)
		{
			_logger = logger;
			_deviceService = deviceService;
		}

		[HttpGet]
		public async Task<Result> Search([FromQuery] DeviceFilterDto filter)
		{
			var pagedData = await _deviceService.SearchAsync(filter);

            return Result.Ok(pagedData);
		}

        [HttpGet("All")]
        public async Task<Result> GetAll([FromQuery] bool activeOnly)
        {
            var devices = await _deviceService.GetAllAsync(activeOnly);

            return Result.Ok(devices);
        }

        [HttpGet("{id}")]
		public async Task<Result> GetById([FromRoute] int id)
		{
			var device = await _deviceService.GetByIdAsync(id);

            return device == null
                ? Result.Error($"Could not find device with ID #{id}")
                : Result.Ok(device);
        }

        [HttpGet("CurrentCost/{deviceId}")]
        public async Task<Result> GetCurrentCost([FromRoute] int deviceId)
        {
            var deviceCost = await _deviceService.GetCurrentCostAsync(deviceId);

            return deviceCost == null
                ? Result.Error($"Could not find current cost for device #{deviceId}")
                : Result.Ok(deviceCost);
        }

        [HttpGet("PriceHistory/{deviceId}")]
        public async Task<Result> GetPriceHistory([FromRoute] int deviceId)
        {
            var deviceCostHistory = await _deviceService.GetPriceHistoryAsync(deviceId);

            return Result.Ok(deviceCostHistory);
        }

        [HttpPost]
        public async Task<Result> CreateDevice([FromBody] DeviceWithCostEditDto model)
        {
            var validator = new DeviceWithCostEditDtoValidator(_deviceService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            model.VendorCost = model.VendorCost > 0 ? model.VendorCost : model.AppliedCost;
            
            if (!await _deviceService.CreateAsync(model))
            {
                return Result.Error("Something went wrong when creating device. Please try again.");
            }

            return Result.Ok($"Created new device successful.");
        }

        [HttpPut("{id}")]
        public async Task<Result> UpdateDevice([FromRoute] int id, [FromBody] DeviceWithCostEditDto model)
        {
            // Make sure device ID is from the route
            model.Id = id;

            var validator = new DeviceWithCostEditDtoValidator(_deviceService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            model.VendorCost = model.VendorCost > 0 ? model.VendorCost : model.AppliedCost;

            if (!await _deviceService.UpdateAsync(model))
            {
                return Result.Error($"Something went wrong when updating device #{id}. Please try again.");
            }

            return Result.Ok($"Updated device #{model.Id} successful.");
        }

        [HttpPatch("{id}")]
        public async Task<Result> UpdateDeviceField([FromRoute] int id, [FromBody] FieldUpdateDto model)
        {
            // Make sure device ID is from the route
            model.Id = id;

            if (!await _deviceService.UpdateFieldAsync(model))
            {
                return Result.Error($"Something went wrong when updating device #{id}. Please try again.");
            }

            return Result.Ok($"Updated device #{id} successful.");
        }

        [HttpPatch("ToggleFlag/{deviceId}")]
		public async Task<Result> ToggleFlag([FromRoute] int deviceId, [FromBody] FlagToggleDto model)
		{
            // Make sure device ID is from the route
            model.Id = deviceId;

			if (!await _deviceService.ToggleFlagAsync(model))
            {
                return Result.Error($"Something went wrong when updating device #{deviceId}. Please try again.");
            }

            return Result.Ok($"Updated device #{deviceId} successful.");
        }

        [HttpPut("Costs/{deviceId}")]
		public async Task<Result> ChangeCost([FromRoute] int deviceId, [FromBody] DeviceCostEditDto model)
		{
            // Make sure device id is from the route
            model.DeviceId = deviceId;

            var validator = new DeviceCostEditDtoValidator(_deviceService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            model.VendorCost = model.VendorCost > 0 ? model.VendorCost : model.AppliedCost;
            
            if (!await _deviceService.UpdateCostAsync(model))
            {
                return Result.Error($"Something went wrong when updating device #{deviceId}. Please try again.");
            }

            return Result.Ok($"Updated device #{deviceId} successful.");
        }
    }
}
