using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface ICustomerService
    {
        Task<ResponseModel<List<CustomerViewModel>>> SearchCustomersAsync(
            CustomerFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<CustomerViewModel>>> GetAllCustomersAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<CustomerViewModel>> GetCustomerByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<CustomerViewModel>>> SearchVendorsAsync(
            CustomerFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<CustomerViewModel>>> GetAllVendorsAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<ResponseModel<CustomerViewModel>> GetVendorByIdAsync(
            int id, CancellationToken cancellationToken = default);
    }
}
