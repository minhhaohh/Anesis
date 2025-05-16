using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.Employees;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<Account> _accountRepo;

        public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _employeeRepo = _unitOfWork.GetRepository<Employee>();
            _accountRepo = _unitOfWork.GetRepository<Account>();
        }

        public async Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepo.All(true)
                .Include(x => x.Manager)
                .Include(x => x.Location)
                .Include(x => x.JobRole)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetByAccountIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var account = await _accountRepo.All(true)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            var employeeId = account?.EmployeeId;

            if (employeeId == null)
            {
                return null;
            }

            return await GetByIdAsync(employeeId.Value, cancellationToken);
        }

        public async Task<Dictionary<int, string>> GetEmployeesAsync(bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.All(true)
                .WhereIf(x => x.Status == EmployeeStatus.Active, activeOnly)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToDictionaryAsync(x => x.Id, x => x.FullName, cancellationToken);
        }

        // SUPPORT METHODS
        private async Task<Employee> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _employeeRepo.FindAsync(cancellationToken, id);
        }
    }
}
