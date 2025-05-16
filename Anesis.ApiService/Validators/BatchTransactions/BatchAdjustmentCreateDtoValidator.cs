using Anesis.ApiService.Domain.DTOs.BatchTransactions;
using FluentValidation;

namespace Anesis.ApiService.Validators.BatchTransactions
{
    public class BatchAdjustmentCreateDtoValidator : AbstractValidator<BatchAdjustmentCreateDto>
    {
        public BatchAdjustmentCreateDtoValidator() 
        {
            RuleFor(x => x.LocationId)
                .NotNull()
                .WithMessage("The Location field is required.");

            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .WithMessage("The Payment Type field is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The Amount field must be greater than 0.");
        }
    }
}
