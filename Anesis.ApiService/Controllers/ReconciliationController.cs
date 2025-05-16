using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.Deposits;
using Anesis.ApiService.Extensions;
using Anesis.ApiService.Services.IServices;
using Anesis.ApiService.Validators.BankTransactions;
using Anesis.ApiService.Validators.BatchTransactions;
using Microsoft.AspNetCore.Mvc;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class ReconciliationController : ControllerBase
    {
        private readonly ILogger<ReconciliationController> _logger;
        private readonly IReconciliationService _reconciliationService;
        private readonly IBankTransactionService _bankTranService;
        private readonly IBatchTransactionService _batchTranService;
        private readonly ICreditTransactionService _creditTranService;
        private readonly IDepositService _depositService;

        public ReconciliationController(
            ILogger<ReconciliationController> logger,
            IReconciliationService reconciliationService,
            IBankTransactionService bankTranService,
            IBatchTransactionService batchTranService,
            ICreditTransactionService creditTranService,
            IDepositService depositService)
        {
            _logger = logger;
            _reconciliationService = reconciliationService;
            _bankTranService = bankTranService;
            _batchTranService = batchTranService;
            _creditTranService = creditTranService;
            _depositService = depositService;
        }

        // RECONCILIATIONS

        [HttpGet("All")]
        public async Task<Result> GetAllReconciliations()
        {
            var reconciliations = await _reconciliationService.GetAllAsync();

            return Result.Ok(reconciliations);
        }

        [HttpGet("LastTime")]
        public async Task<Result> GetLastTimeReconciliation()
        {
            var reconciliation = await _reconciliationService.GetLastTimeAsync();

            return Result.Ok(reconciliation);
        }

        [HttpPost]
        public async Task<Result> CreateReconciliation([FromBody] DateTime? reconciledDate)
        {
            if (!await _reconciliationService.CreateReconciliation(reconciledDate.Value))
            {
                return Result.Error($"Something went wrong when creating reconciliation. Please try again.");
            }

            return Result.Ok("Created new reconciliation successful.");
        }

        [HttpDelete("{id}")]
        public async Task<Result> UndoReconciliation([FromBody] int id)
        {
            if (!await _reconciliationService.UndoReconciliation(id))
            {
                return Result.Error($"Something went wrong when undoing reconciliation. Please try again.");
            }

            return Result.Ok("Undone reconciliation successful.");
        }

        // BANK TRANSACTIONS

        [HttpGet("Banks")]
        public async Task<Result> SearchBanks([FromQuery] BankTransactionFilterDto filter)
        {
            var pagedData = await _bankTranService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("Banks/{id}")]
        public async Task<Result> GetBankById([FromRoute] int id)
        {
            var bankTransaction = await _bankTranService.GetByIdAsync(id);

            return Result.Ok(bankTransaction);
        }

        [HttpGet("Banks/BalanceSummary")]
        public async Task<Result> GetBalanceSummary([FromQuery] BankTransactionFilterDto filter)
        {
            var data = await _bankTranService.GetBalanceSummaryAsync(filter);

            return Result.Ok(data);
        }

        [HttpPost("Banks/Adjustments")]
        public async Task<Result> CreateBankAdjustment([FromBody] BankAdjustmentCreateDto model)
        {
            var validator = new BankAdjustmentCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _bankTranService.CreateAdjustmentAsync(model))
            {
                return Result.Error($"Something went wrong when creating bank transaction adjustment. Please try again.");
            }

            return Result.Ok("Created new bank transaction adjustment successful.");
        }

        [HttpPut("Banks/Split/{id}")]
        public async Task<Result> SplitBankTransaction([FromRoute] int id, [FromBody] BankTransactionSplitDto model)
        {
            // Make sure BankTransactionId is from the route
            model.Id = id;

            var validator = new BankTransactionSplitDtoValidator(_bankTranService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _bankTranService.SplitAsync(model))
            {
                return Result.Error($"Something went wrong when splitting bank transaction #{id}. Please try again.");
            }

            return Result.Ok($"Split bank transaction #{id} successful.");
        }

        // BATCH TRANSACTIONS

        [HttpGet("Batches")]
        public async Task<Result> SearchBatches([FromQuery] BatchTransactionFilterDto filter)
        {
            var pagedData = await _batchTranService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("Batches/{id}")]
        public async Task<Result> GetBatchById([FromRoute] int id)
        {
            var batchTransaction = await _batchTranService.GetByIdAsync(id);

            return Result.Ok(batchTransaction);
        }

        [HttpGet("Batches/ByBankId/{bankId}")]
        public async Task<Result> GetBatchesByBankId([FromRoute] int bankId)
        {
            var batchTransactions = await _batchTranService.GetByBankIdAsync(bankId);

            return Result.Ok(batchTransactions);
        }

        [HttpPut("Batches/LinkBankId/{id}")]
        public async Task<Result> LinkBankIdToBatchTransaction([FromRoute] int id, [FromBody] BatchLinkBankIdDto model)
        {
            // Make sure BatchTransactionId is from the route
            model.BatchTransactionId = id;

            var validator = new BatchLinkBankIdDtoValidator(_batchTranService, _bankTranService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _batchTranService.LinkBankIdAsync(model))
            {
                return Result.Error($"Something went wrong when linking bank transaction to batch transaction #{id}. Please try again.");
            }

            return Result.Ok($"Linked bank transaction to batch transaction #{id} successful.");
        }

        [HttpPut("Batches/UnlinkBankId/{id}")]
        public async Task<Result> UnlinkBankIdFromBatchTransaction([FromRoute] int id)
        {
            if (!await _batchTranService.RemoveLinkedBankIdAsync(id))
            {
                return Result.Error($"Something went wrong when removing bank transaction from batch transaction #{id}. Please try again.");
            }

            return Result.Ok($"Unlinked bank transaction from batch transaction #{id} successful.");
        }

        [HttpPost("Batches/Adjustments")]
        public async Task<Result> CreateBatchAdjustment([FromBody] BatchAdjustmentCreateDto model)
        {
            var validator = new BatchAdjustmentCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _batchTranService.CreateAdjustmentAsync(model))
            {
                return Result.Error($"Something went wrong when creating batch transaction adjustment. Please try again.");
            }

            return Result.Ok("Created new batch transaction adjustment successful.");
        }

        [HttpPut("Batches/Split/{id}")]
        public async Task<Result> SplitBatchTransaction([FromRoute] int id, [FromBody] BatchTransactionSplitDto model)
        {
            // Make sure BatchTransactionId is from the route
            model.Id = id;

            var validator = new BatchTransactionSplitDtoValidator(_batchTranService);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                return validationResult.ToResponseWithErrors();
            }

            if (!await _batchTranService.SplitAsync(model))
            {
                return Result.Error($"Something went wrong when splitting batch transaction #{id}. Please try again.");
            }

            return Result.Ok($"Split batch transaction #{id} successful.");
        }

        // CREDIT TRANSACTIONS

        [HttpGet("Credits/ByBankId/{bankId}")]
        public async Task<Result> GetCreditsByBankId([FromRoute] int bankId)
        {
            var creditTransactions = await _creditTranService.GetByBankIdAsync(bankId);

            return Result.Ok(creditTransactions);
        }

        // DEPOSITS

        [HttpGet("Deposits")]
        public async Task<Result> SearchDeposits([FromQuery] DepositFilterDto filter)
        {
            var pagedData = await _depositService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("Deposits/{id}")]
        public async Task<Result> GetDepositById([FromRoute] int id)
        {
            var deposit = await _depositService.GetByIdAsync(id, true);

            if (deposit == null)
            {
                return Result.Error($"Could not find deposit with id #{id}.");
            }

            return Result.Ok(deposit);
        }

        [HttpGet("Deposits/ByBankId/{bankId}")]
        public async Task<Result> GetDepositByBankId([FromRoute] int bankId)
        {
            var deposit = await _depositService.GetByBankIdAsync(bankId, true);

            if (deposit == null)
            {
                return Result.Error($"Could not find deposit with bank id #{bankId}.");
            }

            return Result.Ok(deposit);
        }
    }
}
