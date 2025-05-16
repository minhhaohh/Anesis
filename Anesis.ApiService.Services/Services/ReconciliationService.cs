using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Domain.DTOs.Reconciliations;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class ReconciliationService : IReconciliationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Reconciliation> _reconciliationRepo;
        private readonly IRepository<BankTransaction> _bankTranRepo;
        private readonly IRepository<BatchTransaction> _batchTranRepo;
        private readonly IBankTransactionService _bankTranService;

        public ReconciliationService(IMapper mapper, IUnitOfWork unitOfWork, IBankTransactionService bankTranService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reconciliationRepo = _unitOfWork.GetRepository<Reconciliation>();
            _bankTranRepo = _unitOfWork.GetRepository<BankTransaction>();
            _batchTranRepo = _unitOfWork.GetRepository<BatchTransaction>();
            _bankTranService = bankTranService;
        }

        public async Task<List<ReconciliationDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _reconciliationRepo.All(true);

            return await _mapper.ProjectTo<ReconciliationDto>(query)
                .OrderByDescending(x => x.ReconciledThru)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReconciliationDto> GetLastTimeAsync(CancellationToken cancellationToken = default)
        {
            var lastTime = await _reconciliationRepo.All(true)
                .OrderByDescending(x => x.ReconciledThru)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<ReconciliationDto>(lastTime);
        }

        public async Task<bool> CreateReconciliation(DateTime reconciledDate, CancellationToken cancellationToken = default)
        {
            var bankTransactionIds = await _bankTranRepo.All(true)
                .Where(x => x.ReconciliationId == null && x.PostDate <= reconciledDate)
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var batchTransactionIds = await _batchTranRepo.All(true)
                .Where(x => x.ReconciliationId == null && x.BankTransactionId > 0 && bankTransactionIds.Contains(x.BankTransactionId.Value))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var condition = new BankTransactionFilterDto() { ReconciledDate = reconciledDate };
            var balanceSummary = await _bankTranService.GetBalanceSummaryAsync(condition, cancellationToken);

            return true;
        }

        public async Task<bool> UndoReconciliation(int id, CancellationToken cancellationToken = default)
        {
            var reconciliation = await FindByIdAsync(id, cancellationToken);

            if (reconciliation == null)
            {
                return false;
            }

            return true;
        }

        // SUPPORT METHODS
        private async Task<Reconciliation> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _reconciliationRepo.FindAsync(cancellationToken, id);
        }
    }
}
