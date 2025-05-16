using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Customers;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;

        public CustomersController(
            ILogger<CustomersController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] CustomerFilterDto filter)
        {
            var pagedData = await _customerService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("All")]
        public async Task<Result> GetAll([FromQuery] bool activeOnly)
        {
            var devices = await _customerService.GetAllAsync(activeOnly);

            return Result.Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            return customer == null
                ? Result.Error($"Could not find customer with ID #{id}.")
                : Result.Ok(customer);
        }

        [HttpGet("Vendors")]
        public async Task<Result> SearchVendors([FromQuery] CustomerFilterDto filter)
        {
            // Make sure CustomerTypeId is Suppliers
            filter.CustomerTypeId = CustomerType.Suppliers;

            var pagedData = await _customerService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("Vendors/All")]
        public async Task<Result> GetAllVendors([FromQuery] bool activeOnly)
        {
            var devices = await _customerService.GetAllByTypeAsync(CustomerType.Suppliers, activeOnly);

            return Result.Ok(devices);
        }

        [HttpGet("Vendors/{id}")]
        public async Task<Result> GetVendorById([FromRoute] int id)
        {
            var customer = await _customerService.GetByTypeByIdAsync(CustomerType.Suppliers, id);

            return customer == null
                ? Result.Error($"Could not find vendor with ID #{id}.")
                : Result.Ok(customer);
        }
    }
}
