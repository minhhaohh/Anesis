using Anesis.ApiService.Domain.DTOs.Accounts;
using FluentValidation;

namespace Anesis.ApiService.Validators.Accounts
{
    public class AccountLoginDtoValidator : AbstractValidator<AccountLoginDto>
    {
        public AccountLoginDtoValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("The UserName field is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The Password field is required.");
        }
    }
}
