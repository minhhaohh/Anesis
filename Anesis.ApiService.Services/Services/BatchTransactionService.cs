using Anesis.ApiService.Domain.DTOs.BatchTransactions;
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
    public class BatchTransactionService : IBatchTransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<BatchTransaction> _batchTranRepo;
        private readonly IRepository<BankTransaction> _bankTranRepo;

        public BatchTransactionService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _batchTranRepo = _unitOfWork.GetRepository<BatchTransaction>();
            _bankTranRepo = _unitOfWork.GetRepository<BankTransaction>();
        }

        public async Task<IPagedList<BatchTransactionDto>> SearchAsync(
            BatchTransactionFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = Search(filter);

            return await _mapper.ProjectTo<BatchTransactionDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(BatchTransaction.PostDate))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<List<BatchTransactionDto>> GetByBankIdAsync(int bankId, CancellationToken cancellationToken = default)
        {
            var query = _batchTranRepo.All(true)
                .Where(x => x.BankTransactionId == bankId)
                .OrderBy(x => x.PostDate);

            return await _mapper.ProjectTo<BatchTransactionDto>(query)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<BatchTransactionDto>> GetByBankIdsAsync(List<int> bankIds, CancellationToken cancellationToken = default)
        {
            var query = _batchTranRepo.All(true)
                .Where(x => x.BankTransactionId > 0 && bankIds.Contains(x.BankTransactionId.Value))
                .OrderBy(x => x.PostDate);

            return await _mapper.ProjectTo<BatchTransactionDto>(query)
                .ToListAsync(cancellationToken);
        }

        public async Task<BatchTransactionDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<BatchTransactionDto>(await FindByIdAsync(id, cancellationToken));
        }

        public async Task<List<int>> GetAutoLinkedItemIdsByBankId(int bankId, CancellationToken cancellationToken = default)
        {
            return await _batchTranRepo.All(true)
                .Where(x => x.BankTransactionId == bankId && x.LinkToItemId > 0)
                .Select(x => x.LinkToItemId.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> LinkBankIdAsync(BatchLinkBankIdDto model, CancellationToken cancellationToken = default)
        {
            var batch = await FindByIdAsync(model.BatchTransactionId, cancellationToken);
            if (batch == null)
            {
                return false;
            }

            var bank = await _bankTranRepo.FindAsync(cancellationToken, model.BankTransactionId);
            if (bank == null)
            {
                return false;
            }

            batch.BankTransactionId = model.BankTransactionId;

            _batchTranRepo.Update(batch);

            var balance = await _batchTranRepo.All(true)
                .Where(x => x.BankTransactionId == model.BankTransactionId)
                .SumAsync(x => x.PaymentAmount, cancellationToken);

            bank.ReconciledFlag = bank.Credit == balance;

            _bankTranRepo.Update(bank);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> RemoveLinkedBankIdAsync(int batchId, CancellationToken cancellationToken = default)
        {
            var batch = await FindByIdAsync(batchId, cancellationToken);
            if (batch == null)
            {
                return false;
            }

            var oldLinkedBankId = batch.BankTransactionId;

            batch.BankTransactionId = null;

            _batchTranRepo.Update(batch);


            if (oldLinkedBankId > 0)
            {
                var oldBank = await _bankTranRepo.FindAsync(cancellationToken, oldLinkedBankId);

                var balance = await _batchTranRepo.All(true)
                    .Where(x => x.BankTransactionId == oldLinkedBankId)
                    .SumAsync(x => x.PaymentAmount, cancellationToken);

                oldBank.ReconciledFlag = oldBank.Credit == balance;

                _bankTranRepo.Update(oldBank);
            }

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> CreateAdjustmentAsync(BatchAdjustmentCreateDto model, CancellationToken cancellationToken = default)
        {
            var batchTransaction = model.ToBatchTransaction();

            await _batchTranRepo.InsertAsync(batchTransaction, cancellationToken);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SplitAsync(BatchTransactionSplitDto model, CancellationToken cancellationToken = default)
        {
            var batchTran = await FindByIdAsync(model.Id, cancellationToken);
            if (batchTran == null)
            {
                return false;
            }

            var currentTime = DateTime.Now;
            var date = currentTime.ToString("yyyyMMdd").ToInt();
            var time = currentTime.ToString("HHmmss").ToInt();

            var secondBatchTran = new BatchTransaction()
            {
                PostDate = batchTran.PostDate,
                BatchOwner = batchTran.BatchOwner,
                BatchId = batchTran.BatchId.HasValue() ? $"S-{batchTran.BatchId}" : "SPLITTED",
                PaymentFrom = $"{batchTran.PaymentFrom} - SPLITTED",
                PaymentType = batchTran.PaymentType,
                PaymentAmount = model.SplitAmount,
                ReconciledFlag = false,
                ReconciliationId = null,
                BankTransactionId = null,
                Description = "SPLITTED",
                LocationId = batchTran.LocationId,
                TransactionType = batchTran.TransactionType,
                DocNo = DateTime.Now.Ticks.ToString(),
                SourceSite = batchTran.SourceSite,
                ClaimNo = batchTran.ClaimNo != null ? date : batchTran.ClaimNo,
                PaymentId = batchTran.PaymentId != null ? time : batchTran.PaymentId
            };

            await _batchTranRepo.InsertAsync(secondBatchTran, cancellationToken);

            var splitDesc = " | SPLITED";
            batchTran.Description = batchTran.Description.Contains(splitDesc) ? $"{batchTran.Description}{splitDesc}" : batchTran.Description;
            batchTran.PaymentAmount -= model.SplitAmount;

            _batchTranRepo.Update(batchTran);

            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }

        // SUPPORT METHODS
        private IQueryable<BatchTransaction> Search(BatchTransactionFilterDto filter)
        {
            var query = filter.ReconciliationId > 0
                ? _batchTranRepo.All(true)
                    .Where(x => x.ReconciliationId == filter.ReconciliationId && x.PaymentAmount != 0)
                : _batchTranRepo.All(true)
                    .Where(x => x.ReconciliationId == null && x.PaymentAmount != 0);

            query = query
                .WhereIf(x => x.PostDate >= filter.FromDate, filter.FromDate != null)
                .WhereIf(x => x.BatchId.Contains(filter.SearchText) || x.PaymentFrom.Contains(filter.SearchText), filter.SearchText.HasValue());

            switch (filter.BatchType)
            {
                case TransactionType.EFT:
                    query = query.Where(x => x.PaymentType == TransactionType.EFT);
                    break;
                case TransactionType.DEPOSIT:
                    query = query.Where(x => x.PaymentType == TransactionType.CASH || x.PaymentType == TransactionType.CHECK);
                    break;
                case TransactionType.CARD:
                    query = query.Where(x => x.PaymentType == TransactionType.CARD);
                    break;
            }

            return query;
        }

        public async Task<BatchTransaction> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _batchTranRepo.FindAsync(cancellationToken, id);
        }
    }
}
