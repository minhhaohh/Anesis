using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.BatchTransactions
{
    public class BatchTransactionSplitDtoValidator : AbstractValidator<BatchTransactionSplitDto>
    {
        private readonly IBatchTransactionService _batchTranService;

        public BatchTransactionSplitDtoValidator(IBatchTransactionService batchTranService)
        {
            _batchTranService = batchTranService;

            RuleFor(x => x.Id)
                .MustAsync(BatchTransactionExistedAsync)
                .WithMessage(x => $"Could not find batch transaction with ID #{x.Id}.");

            RuleFor(x => x.SplitAmount)
                .GreaterThan(0)
                .WithMessage("The Split Amount field must be greater than 0.");
        }

        private async Task<bool> BatchTransactionExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _batchTranService.GetByIdAsync(id, cancellationToken) != null;
        }
    }
}
