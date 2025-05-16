using Anesis.ApiService.Domain.DTOs.Accounts;
using Anesis.ApiService.Domain.DTOs.Employees;

namespace Anesis.ApiService.Infrastructures
{
    public interface IUserContext
    {
        public string AccountId { get; }

        public string UserName { get; }

        Task<AccountDto> GetAccountAsync();

        Task<EmployeeDto> GetEmployeeAsync();

        Task<bool> HasPermissionAsync(int permUniqueId);

        Task<bool> HasPermissionAsync(Enum permission);

        Task<bool> HasAnyPermissionsAsync(params Enum[] permissions);
    }
}
