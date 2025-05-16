using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.BatchTransactions
{
    public class BatchLinkBankIdDtoValidator : AbstractValidator<BatchLinkBankIdDto>
    {
        private readonly IBatchTransactionService _batchTranService;
        private readonly IBankTransactionService _bankTranService;

        public BatchLinkBankIdDtoValidator(IBatchTransactionService batchTranService, IBankTransactionService bankTranService)
        {
            _batchTranService = batchTranService;
            _bankTranService = bankTranService;

            RuleFor(x => x.BatchTransactionId)
                .MustAsync(BatchTransactionExistedAsync)
                .WithMessage(x => $"Could not find batch transaction with ID #{x.BatchTransactionId}.");

            RuleFor(x => x.BankTransactionId)
                .MustAsync(BankTransactionExistedAsync)
                .WithMessage(x => $"Could not find bank transaction with ID #{x.BankTransactionId}.");
        }

        private async Task<bool> BatchTransactionExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _batchTranService.GetByIdAsync(id, cancellationToken) != null;
        }

        private async Task<bool> BankTransactionExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _bankTranService.GetByIdAsync(id, cancellationToken) != null;
        }
    }
}
