using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;

        public PatientsController(
            ILogger<PatientsController> logger,
            IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var patient = await _patientService.GetByIdAsync(id);

            return patient == null
                ? Result.Error($"Could not find patient with id #{id}")
                : Result.Ok(patient);
        }

        [HttpGet("ByChartNo/{chartNo}")]
        public async Task<Result> GetByChartNo([FromRoute] string chartNo)
        {
            var patient = await _patientService.GetByChartNoAsync(chartNo);

            return patient == null
                ? Result.Error($"Could not find patient with chart no #{chartNo}")
                : Result.Ok(patient);
        }
    }
}
