using Anesis.ApiService.Domain.DTOs.CreditTransactions;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class CreditTransactionService : ICreditTransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CreditTransaction> _creditTransRepo;

        public CreditTransactionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creditTransRepo = _unitOfWork.GetRepository<CreditTransaction>();
        }

        public async Task<List<CreditTransactionDto>> GetByBankIdAsync(int bankId, CancellationToken cancellationToken = default)
        {
            var query = _creditTransRepo.All(true)
                .Where(x => x.BankTransactionId == bankId)
                .OrderBy(x => x.SubmitDate);

            return await _mapper.ProjectTo<CreditTransactionDto>(query)
                .ToListAsync(cancellationToken);
        }
    }
}
