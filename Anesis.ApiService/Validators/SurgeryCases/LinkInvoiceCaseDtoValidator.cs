using Anesis.ApiService.Domain.DTOs.SurgeryCases;
using Anesis.ApiService.Services.IServices;
using FluentValidation;

namespace Anesis.ApiService.Validators.SurgeryCases
{
    public class LinkInvoiceCaseDtoValidator : AbstractValidator<LinkInvoiceCaseDto>
    {
        private readonly ISurgeryCaseService _surgeryCaseService;
        private readonly IInvoiceService _invoiceService;

        public LinkInvoiceCaseDtoValidator(
            ISurgeryCaseService surgeryCaseService, 
            IInvoiceService invoiceService)
        {
            _surgeryCaseService = surgeryCaseService;
            _invoiceService = invoiceService;

            RuleFor(x => x.CaseId)
                .MustAsync(SurgeryCaseExistedAsync)
                .WithMessage(x => $"Could not find surgery case with ID #{x.CaseId}.");

            RuleFor(x => x.InvoiceId)
                .GreaterThan(0)
                .WithMessage("Please select the Linked Invoice field.")
                .MustAsync(InvoiceExistedAsync)
                .WithMessage(x => $"Could not find invoice with ID #{x.CaseId}.");

            RuleFor(x => x.SeparateAmount)
                .MustAsync(RequiredIfBulkInvoiceAsync)
                .WithMessage(x => $"Please enter a separate amount to the linked invoice for surgery case #{x.CaseId}.");
        }

        private async Task<bool> SurgeryCaseExistedAsync(int id, CancellationToken cancellationToken)
        {
            return await _surgeryCaseService.GetByIdAsync(id, cancellationToken) != null;
        }

        private async Task<bool> InvoiceExistedAsync(int invoiceId, CancellationToken cancellationToken)
        {
            return await _invoiceService.GetByIdAsync(invoiceId, false, cancellationToken) != null;
        }

        private async Task<bool> RequiredIfBulkInvoiceAsync(LinkInvoiceCaseDto model, decimal separateAmount, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceService.GetByIdAsync(model.InvoiceId, false, cancellationToken);

            return invoice == null
                || !invoice.IsBulk
                || separateAmount > 0;
        }
    }
}
