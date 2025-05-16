using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Domain.DTOs.Locations;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly ILocationService _locationService;

        public LocationsController(
            ILogger<LocationsController> logger,
            ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] LocationFilterDto filter)
        {
            var pagedData = await _locationService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var location = await _locationService.GetByIdAsync(id);

            return location == null
                ? Result.Error($"Could not find location with ID #{id}")
                : Result.Ok(location);
        }
    }
}
