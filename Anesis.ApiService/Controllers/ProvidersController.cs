using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Domain.DTOs.Providers;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class ProvidersController : ControllerBase
    {
        private readonly ILogger<ProvidersController> _logger;
        private readonly IProviderService _providerService;

        public ProvidersController(
            ILogger<ProvidersController> logger,
            IProviderService providerService)
        {
            _logger = logger;
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] ProviderFilterDto filter)
        {
            var pagedData = await _providerService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }
    }
}
