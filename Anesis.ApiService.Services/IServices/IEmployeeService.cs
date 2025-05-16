using Anesis.ApiService.Domain.DTOs.Employees;
using Anesis.Shared.Models;

namespace Anesis.ApiService.Services.IServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<EmployeeDto> GetByAccountIdAsync(string userId, CancellationToken cancellationToken = default);

        Task<Dictionary<int, string>> GetEmployeesAsync(
            bool activeOnly = false, CancellationToken cancellationToken = default);
    }
}
