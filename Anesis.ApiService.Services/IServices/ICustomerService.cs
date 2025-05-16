using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Customers;
using Anesis.Shared.Constants;

namespace Anesis.ApiService.Services.IServices
{
    public interface ICustomerService
    {
        Task<IPagedList<CustomerDto>> SearchAsync(CustomerFilterDto filter,
            CancellationToken cancellationToken = default);

        Task<List<CustomerDto>> GetAllAsync(bool activeOnly = false, 
            CancellationToken cancellationToken = default);

        Task<List<CustomerDto>> GetAllByTypeAsync(CustomerType customerTypeId,
            bool activeOnly = false, CancellationToken cancellationToken = default);

        Task<CustomerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<CustomerDto> GetByTypeByIdAsync(CustomerType customerTypeId, 
            int id, CancellationToken cancellationToken = default);
    }
}
