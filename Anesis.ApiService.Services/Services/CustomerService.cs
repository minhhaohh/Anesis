using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Customers;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Constants;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customerRepo;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepo = _unitOfWork.GetRepository<Customer>();
        }

        public async Task<IPagedList<CustomerDto>> SearchAsync(CustomerFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            var query = Search(filter);

            return await _mapper.ProjectTo<CustomerDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(CustomerDto.CustomerName))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<List<CustomerDto>> GetAllAsync(bool activeOnly = false,
            CancellationToken cancellationToken = default)
        {
            var query = _customerRepo.All(true)
                .WhereIf(x => x.IsActive, activeOnly);

            return await _mapper.ProjectTo<CustomerDto>(query)
                .OrderBy(x => x.CustomerName)
                .ThenBy(x => x.Description)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<CustomerDto>> GetAllByTypeAsync(CustomerType customerTypeId,
            bool activeOnly = false, CancellationToken cancellationToken = default)
        {
            var query = _customerRepo.All(true)
                .Where(x => x.CustomerTypeId == customerTypeId)
                .WhereIf(x => x.IsActive, activeOnly);

            return await _mapper.ProjectTo<CustomerDto>(query)
                .OrderBy(x => x.CustomerName)
                .ThenBy(x => x.Description)
                .ToListAsync(cancellationToken);
        }

        public async Task<CustomerDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var customer = await FindByIdAsync(id, cancellationToken);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetByTypeByIdAsync(CustomerType customerTypeId,
            int id, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepo.All(true)
                .FirstOrDefaultAsync(x => x.CustomerTypeId == customerTypeId && x.Id == id);

            return _mapper.Map<CustomerDto>(customer);
        }

        // SUPPORT METHODS 
        private IQueryable<Customer> Search(CustomerFilterDto filter)
        {
            return _customerRepo.All(true)
                .WhereIf(x => x.CustomerTypeId == filter.CustomerTypeId, filter.CustomerTypeId != null)
                .WhereIf(x => x.CustomerName.Contains(filter.CustomerName), filter.CustomerName.HasValue())
                .WhereIf(x => x.IsActive, filter.ActiveOnly);
        }

        private async Task<Customer> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _customerRepo.FindAsync(cancellationToken, id);
        }
    }
}
