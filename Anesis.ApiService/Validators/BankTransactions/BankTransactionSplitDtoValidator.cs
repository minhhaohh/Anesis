using Anesis.ApiService.Domain.DTOs.BankTransactions;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.BankTransactions
{
    public class BankTransactionSplitDtoValidator : AbstractValidator<BankTransactionSplitDto>
    {
        private readonly IBankTransactionService _bankTranService;

        public BankTransactionSplitDtoValidator(IBankTransactionService bankTranService)
        {
            _bankTranService = bankTranService;

            RuleFor(x => x.Id)
                .MustAsync(BankTransactionExistedAsync)
                .WithMessage(x => $"Could not find bank transaction with ID #{x.Id}.");

            RuleFor(x => x.SplitAmount)
                .GreaterThan(0)
                .WithMessage("The Split Amount field must be greater than 0.");

            RuleFor(x => x.Notes)
                .NotEmpty()
                .WithMessage("The Notes field is required.");
        }

        private async Task<bool> BankTransactionExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _bankTranService.GetByIdAsync(id, cancellationToken) != null;
        }
    }
}
