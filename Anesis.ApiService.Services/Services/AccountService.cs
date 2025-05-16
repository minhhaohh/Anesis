using Anesis.ApiService.Domain.DTOs.Accounts;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anesis.ApiService.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Account> _accountRepo;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountRepo = _unitOfWork.GetRepository<Account>();
        }

        public async Task<AccountDto> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var account = await FindByIdAsync(id, cancellationToken);

            return _mapper.Map<AccountDto>(account);
        }

        // SUPPORT METHODS
        private async Task<Account> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _accountRepo.FindAsync(cancellationToken, id);
        }
    }
}
