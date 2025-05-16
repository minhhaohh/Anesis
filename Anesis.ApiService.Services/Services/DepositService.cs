using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Deposits;
using Anesis.ApiService.Domain.Entities;
using Anesis.ApiService.Domain.Extensions;
using Anesis.ApiService.EntityFrameworkCore.EntityFrameworkCore;
using Anesis.ApiService.Services.IServices;
using Anesis.Shared.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Anesis.ApiService.Services.Services
{
    public class DepositService : IDepositService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DepositBatch> _depositRepo;
        private readonly IRepository<DepositBatchItem> _depositItemRepo;
        private readonly IBatchTransactionService _batchTranService;

        public DepositService(IMapper mapper, IUnitOfWork unitOfWork, IBatchTransactionService batchTranService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _depositRepo = _unitOfWork.GetRepository<DepositBatch>();
            _depositItemRepo = _unitOfWork.GetRepository<DepositBatchItem>();
            _batchTranService = batchTranService;
        }

        public async Task<IPagedList<DepositDto>> SearchAsync(
            DepositFilterDto filter, CancellationToken cancellationToken = default)
        {
            var query = _depositRepo.All(true)
                .WhereIf(x => x.CreatedDate >= filter.CreatedFromDate, filter.CreatedFromDate != null)
                .WhereIf(x => x.Status == filter.Status, filter.Status.HasValue())
                .OrderBy(x => x.DepositDate);

            return await _mapper.ProjectTo<DepositDto>(query)
                .SortData(filter.Sidx, filter.Sord, nameof(DepositBatch.CreatedDate))
                .ToPageListAsync(filter.StartIndex, filter.CountNumber, cancellationToken);
        }

        public async Task<DepositDto> GetByIdAsync(int id, bool includeDetails, CancellationToken cancellationToken = default)
        {
            var deposit = _mapper.Map<DepositDto>(await FindByIdAsync(id, cancellationToken));

            if (deposit == null)
            {
                return null;
            }

            if (includeDetails)
            {
                var depositItems = await GetDepositItemsAsync(id, cancellationToken);

                if (deposit.BankTransactionId > 0)
                {
                    var linkedItemIds = await _batchTranService.GetAutoLinkedItemIdsByBankId(deposit.BankTransactionId.Value, cancellationToken);

                    foreach (var item in depositItems)
                    {
                        var itemId = item.DepositBatchId * 100 + item.ItemNumber;

                        if (linkedItemIds.Contains(itemId))
                        {
                            item.IsAutoLinked = true;
                        }
                    }
                }

                deposit.DepositItems = depositItems;
            }

            return deposit;
        }

        public async Task<DepositDto> GetByBankIdAsync(int bankId, bool includeDetails, CancellationToken cancellationToken = default)
        {
            var deposit = _mapper.Map<DepositDto>(await FindByBankIdAsync(bankId, cancellationToken));

            if (deposit == null)
            {
                return null;
            }

            if (includeDetails)
            {
                var depositItems = await GetDepositItemsAsync(deposit.Id, cancellationToken);
                var linkedItemIds = await _batchTranService.GetAutoLinkedItemIdsByBankId(bankId, cancellationToken);

                foreach (var item in depositItems)
                {
                    var itemId = item.DepositBatchId * 100 + item.ItemNumber;

                    if (linkedItemIds.Contains(itemId))
                    {
                        item.IsAutoLinked = true;
                    }
                }

                deposit.DepositItems = depositItems;
            }

            return deposit;
        }

        public async Task<List<DepositDto>> GetByBankIdsAsync(List<int> bankIds, CancellationToken cancellationToken = default)
        {
            var query = _depositRepo.All(true)
                .Where(x => x.BankTransactionId > 0 && bankIds.Contains(x.BankTransactionId.Value))
                .OrderBy(x => x.DepositDate);

            return await _mapper.ProjectTo<DepositDto>(query)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DepositItemDto>> GetDepositItemsAsync(int depositId, CancellationToken cancellationToken = default)
        {
            var query = _depositItemRepo.All(true)
                .Where(x => x.DepositBatchId == depositId)
                .OrderBy(x => x.ReceivedDate);

            return await _mapper.ProjectTo<DepositItemDto>(query)
                .ToListAsync(cancellationToken);
        }

        // SUPPORT METHODS
        private async Task<DepositBatch> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _depositRepo.FindAsync(cancellationToken, id);
        }

        private async Task<DepositBatch> FindByBankIdAsync(int bankId, CancellationToken cancellationToken)
        {
            return await _depositRepo.GetFirstAsync(x => x.BankTransactionId == bankId, true, cancellationToken);
        }
    }
}
