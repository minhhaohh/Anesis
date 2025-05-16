using Anesis.ApiService.Domain.Constants;
using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Domain.DTOs.Common;
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
    public class BankTransactionService : IBankTransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BankTransaction> _bankTranRepo;
        private readonly IRepository<BatchTransaction> _batchTranRepo;
        private readonly IRepository<CreditTransaction> _creditRepo;
        private readonly IBatchTransactionService _batchTranService;
        private readonly IDepositService _depositService;

        public BankTransactionService(
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IBatchTransactionService batchTranService,
            IDepositService depositService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bankTranRepo = _unitOfWork.GetRepository<BankTransaction>();
            _batchTranRepo = _unitOfWork.GetRepository<BatchTransaction>();
            _creditRepo = _unitOfWork.GetRepository<CreditTransaction>();
            _batchTranService = batchTranService;
            _depositService = depositService;
        }

        public async Task<IPagedList<BankTransactionDto>> SearchAsync(
            BankTransactionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var result = await _mapper.ProjectTo<BankTransactionDto>(Search(filter))
                .SortData(filter.Sidx, filter.Sord, nameof(BatchTransaction.PostDate))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);

            var bankIds = result.Data.Select(x => x.Id).ToList();

            var batchTrans = await _batchTranService.GetByBankIdsAsync(bankIds, cancellationToken);

            var creditTrans = await _creditRepo.All(true)
                .Where(x => x.BankTransactionId > 0 && bankIds.Contains(x.BankTransactionId.Value))
                .ToListAsync(cancellationToken);

            var deposits = await _depositService.GetByBankIdsAsync(bankIds, cancellationToken);

            foreach (var item in result.Data)
            {
                item.Balance = batchTrans.Where(x => x.BankTransactionId == item.Id).Sum(x => x.PaymentAmount);
                item.HasCreditDetails = creditTrans.Any(x => x.BankTransactionId == item.Id);
                item.HasDepositDetails = deposits.Any(x => x.BankTransactionId == item.Id);
            }

            return result;
        }

        public async Task<BankTransaction> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _bankTranRepo.FindAsync(cancellationToken, id);
        }

        public async Task<BalanceSummaryDto> GetBalanceSummaryAsync(BankTransactionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = Search(filter);

            var bankTotal = await query.SumAsync(x => x.Credit, cancellationToken);
            var bankTranIds = await query.Select(x => x.Id).ToListAsync(cancellationToken);

            var matchedTotal = await _batchTranRepo.All(true)
                .Where(x => x.BankTransactionId > 0 && bankTranIds.Contains(x.BankTransactionId.Value))
                .SumAsync(x => x.PaymentAmount, cancellationToken);

            return new BalanceSummaryDto()
            {
                BankTotal = bankTotal.Value,
                MatchedTotal = matchedTotal
            };
        }

        public async Task<bool> CreateAdjustmentAsync(BankAdjustmentCreateDto model, CancellationToken cancellationToken = default)
        {
            var bankTran = model.ToBankTransaction();

            await _bankTranRepo.InsertAsync(bankTran, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SplitAsync(BankTransactionSplitDto model, CancellationToken cancellationToken = default)
        {
            var bankTran = await FindByIdAsync(model.Id, cancellationToken);
            if (bankTran == null)
            {
                return false;
            }

            var secondBankTran = new BankTransaction()
            {
                PostDate = bankTran.PostDate,
                Reference = bankTran.Reference,
                Description1 = bankTran.Description1,
                Description2 = $"Split from {bankTran.Id} - {model.Notes}",
                TransactionType = bankTran.TransactionType,
                Debit = bankTran.Debit,
                Credit = model.SplitAmount,
                ReconciledFlag = false,
                ReconciliationId = null,
                HashKey = Guid.NewGuid().ToString("N")
            };

            if (bankTran.Description1 == "DEPOSIT" || bankTran.Description1 == "CHECK DEPOSIT PACKAGE" ||
                bankTran.Description1 == "UNIVERSAL CREDIT" || bankTran.Reference.StartsWith("NSFER FRMDEP "))
            {
                secondBankTran.Reference = DateTime.Now.ToUnixTimeSeconds().ToString();
            }

            await _bankTranRepo.InsertAsync(secondBankTran, cancellationToken);

            bankTran.Credit -= model.SplitAmount;

            _bankTranRepo.Update(bankTran);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        // SUPPORT METHODS
        private IQueryable<BankTransaction> Search(BankTransactionFilterDto filter)
        {
            var query = filter.ReconciliationId > 0
                ? _bankTranRepo.All(true)
                    .Where(x => x.ReconciliationId == filter.ReconciliationId && x.Id > 0)
                : _bankTranRepo.All(true)
                    .Where(x => x.ReconciliationId == null && x.Id > 0)
                    .WhereIf(x => x.PostDate <= filter.ReconciledDate, filter.ReconciledDate != null)
                    .WhereIf(x => x.TransactionType == filter.BankType, filter.BankType.HasValue() && filter.BankType != TransactionType.ALL);

            switch (filter.BatchType)
            {
                case TransactionType.EFT:
                    query = query.Where(x => x.Description1 == BankKeywordSettings.AchCreditDescription &&
                                    !x.Description2.Contains(BankKeywordSettings.CreditCardProcessor) &&
                                    !BankKeywordSettings.BankCreditCardReference.Contains(x.Reference) &&
                                    !BankKeywordSettings.BankDepositReference.Contains(x.Reference));
                    break;
                case TransactionType.DEPOSIT:
                    query = query.Where(x => BankKeywordSettings.BankDepositReference.Contains(x.Description1) ||
                                            (x.Description1 == BankKeywordSettings.AchCreditDescription &&
                                                BankKeywordSettings.BankDepositReference.Contains(x.Reference)));
                    break;
                case TransactionType.CARD:
                    query = query.Where(x => x.Description1 == BankKeywordSettings.AchCreditDescription &&
                                            (x.Description2.Contains(BankKeywordSettings.CreditCardProcessor) ||
                                                BankKeywordSettings.BankCreditCardReference.Contains(x.Reference)));
                    break;
            }

            return query;
        }

        private async Task<BankTransaction> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _bankTranRepo.FindAsync(cancellationToken, id);
        }
    }
}
