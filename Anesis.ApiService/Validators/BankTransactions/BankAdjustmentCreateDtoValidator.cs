using Anesis.ApiService.Domain.DTOs.BankTransactions;
using FluentValidation;

namespace Anesis.ApiService.Validators.BankTransactions
{
    public class BankAdjustmentCreateDtoValidator : AbstractValidator<BankAdjustmentCreateDto>
    {
        public BankAdjustmentCreateDtoValidator() 
        {
            RuleFor(x => x.PostDate)
                .NotNull()
                .WithMessage("The Post Date field is required.");

            RuleFor(x => x.Reference)
                .NotEmpty()
                .WithMessage("The Reference field is required.");

            RuleFor(x => x.TransactionType)
                .NotEmpty()
                .WithMessage("The Transaction Type field is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The Amount field must be greater than 0.");
        }
    }
}
