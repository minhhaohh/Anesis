using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.PotentialProcedures;
using Anesis.ApiService.Domain.DTOs.SurgeryCases;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.Proposals;
using Anesis.ApiService.Validators.SurgeryCases;
using Anesis.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class SurgeryCasesController
    {
        private readonly ILogger<SurgeryCasesController> _logger;
        private readonly ISurgeryCaseService _surgeryCaseService;
        private readonly ICustomerService _deviceService;
        private readonly IInvoiceService _invoiceService;

        public SurgeryCasesController(
            ILogger<SurgeryCasesController> logger,
            ISurgeryCaseService surgeryService,
            ICustomerService deviceService,
            IInvoiceService invoiceService)
        {
            _logger = logger;
            _surgeryCaseService = surgeryService;
            _deviceService = deviceService;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] SurgeryCaseFilterDto filter)
        {
            var pagedData = await _surgeryCaseService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var surgeryCase = await _surgeryCaseService.GetByIdAsync(id);

            return Result.Ok(surgeryCase);
        }

        [HttpGet("ChangeLogs/{caseId}")]
        public async Task<Result> GetChangeLogs([FromRoute] int caseId)
        {
            var changeLogs = await _surgeryCaseService.GetChangeLogsAsync(caseId);

            return Result.Ok(changeLogs);
        }

        [HttpPatch("SetStatus/{caseId}")]
        public async Task<Result> SetStatus([FromRoute] int caseId, [FromBody] CaseSetStatusDto model)
        {
            // Make sure surgery case ID is from the route
            model.Id = caseId;

            var validator = new CaseSetStatusDtoValidator(_surgeryCaseService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _surgeryCaseService.SetStatusAsync(model))
            {
                return Result.Error($"Something went wrong when updating surgery case #{caseId}. Please try again.");
            }

            return Result.Ok($"Updated surgery case #{caseId} status to {(SurgeryCaseStatus)model.Status} successful.");
        }

        [HttpPatch("LinkInvoice/{caseId}")]
        public async Task<Result> LinkInvoice([FromRoute] int caseId, [FromBody] CaseLinkInvoiceDto model)
        {
            // Make sure surgery case ID is from the route
            model.CaseId = caseId;

            var validator = new CaseLinkInvoiceDtoValidator(_surgeryCaseService, _invoiceService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _surgeryCaseService.LinkInvoiceAsync(model))
            {
                return Result.Error($"Something went wrong when updating surgery case #{caseId}. Please try again.");
            }

            return Result.Ok($"Linked invoice to surgery case #{caseId} successful.");
        }

        [HttpPatch("UnlinkInvoice/{caseId}")]
        public async Task<Result> UnlinkInvoice([FromRoute] int caseId)
        {
            if (!await _surgeryCaseService.RemoveLinkedInvoiceAsync(caseId))
            {
                return Result.Error($"Something went wrong when updating surgery case #{caseId}. Please try again.");
            }

            return Result.Ok($"Remove linked invoice from surgery case #{caseId} successful.");
        }

        // CASE CPT CODES
        [HttpGet("CaseCptCodes")]
        public async Task<Result> SearchCaseCptCodes([FromQuery] CaseCptCodeFilterDto filter)
        {
            var pagedData = await _surgeryCaseService.SearchCaseCptCodesAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpPatch("CaseCptCodes/{id}")]
        public async Task<Result> UpdateCaseCptCodeField([FromRoute] int id, [FromBody] FieldUpdateDto model)
        {
            // Make sure case CPT code ID is from the route
            model.Id = id;

            if (!await _surgeryCaseService.UpdateCaseCptCodeFieldAsync(model))
            {
                return Result.Error($"Something went wrong when updating surgery case CPT code #{id}. Please try again.");
            }

            return Result.Ok($"Updated surgery case CPT code #{id} successful.");
        }

        [HttpDelete("CaseCptCodes/{id}")]
        public async Task<Result> DeleteCaseCptCode([FromRoute] int id)
        {
            if (!await _surgeryCaseService.DeleteCaseCptCodeAsync(id))
            {
                return Result.Error($"Something went wrong when deleting surgery case CPT code #{id}. Please try again.");
            }

            return Result.Ok($"Deleted surgery case CPT code #{id} successful.");
        }

        // CASE COSTS
        [HttpGet("CaseCosts")]
        public async Task<Result> SearchCaseCosts([FromQuery] CaseCostFilterDto filter)
        {
            var pagedData = await _surgeryCaseService.SearchCaseCostsAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpPost("CaseCosts")]
        public async Task<Result> AddCaseCost([FromBody] CaseCostAddDto model)
        {
            var validator = new CaseCostAddDtoValidator(_surgeryCaseService, _deviceService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _surgeryCaseService.AddCaseCostAsync(model))
            {
                return Result.Error($"Something went wrong when adding device to surgery case #{model.CaseId}. Please try again.");
            }

            return Result.Ok($"Added device #{model.DeviceId} to surgery case #{model.CaseId} successful.");
        }

        [HttpPatch("CaseCosts/{id}")]
        public async Task<Result> UpdateCaseCostField([FromRoute] int id, [FromBody] FieldUpdateDto model)
        {
            // Make sure case cost ID is from the route
            model.Id = id;

            if (!await _surgeryCaseService.UpdateCaseCostFieldAsync(model))
            {
                return Result.Error($"Something went wrong when updating surgery case cost #{id}. Please try again.");
            }

            return Result.Ok($"Updated surgery case cost #{id} successful.");
        }

        [HttpDelete("CaseCosts/{id}")]
        public async Task<Result> DeleteCaseCost([FromRoute] int id)
        {
            if (!await _surgeryCaseService.DeleteCaseCostAsync(id))
            {
                return Result.Error($"Something went wrong when deleting surgery case cost #{id}. Please try again.");
            }

            return Result.Ok($"Deleted surgery case cost #{id} successful.");
        }
    }
}
