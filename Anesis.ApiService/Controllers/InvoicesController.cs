using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Domain.DTOs.GenericInvoices;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class InvoicesController : ControllerBase
    {
        private readonly ILogger<InvoicesController> _logger;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(
            ILogger<InvoicesController> logger,
            IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] InvoiceFilterDto filter)
        {
            var pagedData = await _invoiceService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id, true);

            return invoice == null
                ? Result.Error($"Could not find invoice with ID #{id}")
                : Result.Ok(invoice);
        }

        [HttpGet("ChangeLogs/{id}")]
        public async Task<Result> GetChangeLogs([FromRoute] int id)
        {
            var changeLogs = await _invoiceService.GetChangeLogsAsync(id);

            return Result.Ok(changeLogs);
        }

        [HttpGet("AvailableToLinkToCase")]
        public async Task<Result> GetAvailableInvoicesToLinkToCase([FromQuery] int? currentId, [FromQuery] int? locationId)
        {
            var invoices = await _invoiceService.GetAvailableInvoicesToLinkToCaseAsync(currentId, locationId);

            return Result.Ok(invoices);
        }
    }
}
