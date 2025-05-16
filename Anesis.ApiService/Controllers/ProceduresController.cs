using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Domain.DTOs.Procedures;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class ProceduresController : ControllerBase
    {
        private readonly ILogger<ProceduresController> _logger;
        private readonly IProcedureService _procedureService;

        public ProceduresController(
            ILogger<ProceduresController> logger,
            IProcedureService procedureService)
        {
            _logger = logger;
            _procedureService = procedureService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] ProcedureFilterDto filter)
        {
            var providers = await _procedureService.SearchAsync(filter);

            return Result.Ok(providers);
        }
    }
}
