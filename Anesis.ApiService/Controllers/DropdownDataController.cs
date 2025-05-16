using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class DropdownDataController : ControllerBase
    {
        private readonly ILogger<DropdownDataController> _logger;
        private readonly ILocationService _locationService;
        private readonly IEmployeeService _employeeService;
        private readonly IProviderService _providerService;
        private readonly IProcedureService _procedureService;
        private readonly IInsuranceService _insuranceService;
        private readonly IInvoiceService _invoiceService;

        public DropdownDataController(
            ILogger<DropdownDataController> logger,
            ILocationService locationService,
            IEmployeeService employeeService,
            IProviderService providerService,
            IProcedureService procedureService,
            IInsuranceService insuranceService,
            IInvoiceService invoiceService)
        {
            _logger = logger;
            _locationService = locationService;
            _employeeService = employeeService;
            _providerService = providerService;
            _procedureService = procedureService;
            _insuranceService = insuranceService;
            _invoiceService = invoiceService;
        }

        [HttpGet("Locations")]
        public async Task<Result> GetLocations([FromQuery] bool activeOnly)
        {
            var data = await _locationService.GetLocationsAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("AscLocations")]
        public async Task<Result> GetAscLocations([FromQuery] bool activeOnly)
        {
            var data = await _locationService.GetAscLocationsAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("Employees")]
        public async Task<Result> GetEmployees([FromQuery] bool activeOnly)
        {
            var data = await _employeeService.GetEmployeesAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("Providers")]
        public async Task<Result> GetProviders([FromQuery] bool activeOnly)
        {
            var data = await _providerService.GetProvidersAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("Doctors")]
        public async Task<Result> GetDoctors([FromQuery] bool activeOnly)
        {
            var data = await _providerService.GetDoctorsAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("MidLevelProviders")]
        public async Task<Result> GetMidLevelProviders([FromQuery] bool activeOnly)
        {
            var data = await _providerService.GetMidLevelProvidersAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("Procedures")]
        public async Task<Result> GetProcedures([FromQuery] bool activeOnly)
        {
            var data = await _procedureService.GetProceduresAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("Insurances")]
        public async Task<Result> GetInsurances([FromQuery] bool activeOnly)
        {
            var data = await _insuranceService.GetInsurancesAsync(activeOnly);

            return Result.Ok(data);
        }

        [HttpGet("InvoiceOwners")]
        public async Task<Result> GetInvoiceOwners()
        {
            var data = await _invoiceService.GetAllOwnersAsync();

            return Result.Ok(data);
        }
    }
}
