using Anesis.ApiService.Domain.DTOs.Common;
using FluentValidation.Results;

namespace Anesis.ApiService.Extensions
{
    public static class FluentExtensions
    {
        public static List<string> GetRawMessages(this ValidationResult validationResult)
        {
            var errors = new List<string>();

            foreach (var err in validationResult.Errors)
            {
                errors.AddRange(err.ErrorMessage.Split("<br/>"));
            }

            return errors;
        }

        public static Result ToResponseWithErrors(this ValidationResult validationResult)
        {
            return Result.Error(validationResult.GetRawMessages());
        }
    }
}
