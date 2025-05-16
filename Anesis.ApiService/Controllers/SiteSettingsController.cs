using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.SiteSettings;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class SiteSettingsController : ControllerBase
    {
        private readonly ILogger<SiteSettingsController> _logger;
        private readonly ISiteSettingService _siteSettingService;

        public SiteSettingsController(
            ILogger<SiteSettingsController> logger,
            ISiteSettingService siteSettingService)
        {
            _logger = logger;
            _siteSettingService = siteSettingService;
        }

        [HttpGet("GeneralSettings")]
        public async Task<Result> GetGeneralSettings()
        {
            var data = await _siteSettingService.LoadSettingsAsync<GeneralSettings>();

            return Result.Ok(data);
        }

        [HttpGet("StaffScheduleSettings")]
        public async Task<Result> GetStaffScheduleSettings()
        {
            var data = await _siteSettingService.LoadSettingsAsync<StaffScheduleSettings>();

            return Result.Ok(data);
        }
    }
}
