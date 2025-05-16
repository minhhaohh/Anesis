using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Accounts;
using Anesis.ApiService.Domain.DTOs.Employees;
using Anesis.ApiService.Services.IServices;
using System.Security.Claims;

namespace Anesis.ApiService.Infrastructures
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IEmployeeService _employeeService;
        private readonly IPermissionService _permissionService;

        private AccountDto _cachedAccount = null;
        private EmployeeDto _cachedEmployee = null;

        public UserContext(
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IEmployeeService employeeService,
            IPermissionService permissionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _employeeService = employeeService;
            _permissionService = permissionService;
        }

        protected ClaimsPrincipal CurrentUser => _httpContextAccessor.HttpContext.User;

        public string AccountId => CurrentUser.FindFirstValue(ClaimTypes.Name);

        public string UserName => CurrentUser.FindFirstValue(ClaimTypes.Name);

        public async Task<AccountDto> GetAccountAsync()
        {
            if (_cachedAccount == null)
            {
                _cachedAccount = await _accountService.GetByIdAsync(AccountId);
            }

            return _cachedAccount;
        }

        public async Task<EmployeeDto> GetEmployeeAsync()
        {
            if (_cachedEmployee == null)
            {
                _cachedEmployee = await _employeeService.GetByAccountIdAsync(AccountId);
            }

            return _cachedEmployee;
        }

        public async Task<bool> HasPermissionAsync(int permUniqueId)
        {
            return await _permissionService.HasPermissionAsync(AccountId, permUniqueId);
        }

        public async Task<bool> HasPermissionAsync(Enum permission)
        {
            return await HasPermissionAsync(Convert.ToInt32(permission));
        }

        public async Task<bool> HasAnyPermissionsAsync(params Enum[] permissions)
        {
            foreach (var permission in permissions)
            {
                if (await HasPermissionAsync(Convert.ToInt32(permission)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
