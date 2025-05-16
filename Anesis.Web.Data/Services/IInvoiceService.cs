using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IInvoiceService
    {
        Task<ResponseModel<List<InvoiceViewModel>>> SearchInvoicesAsync(
            InvoiceFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<InvoiceViewModel>> GetInvoiceByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<ChangeLogViewModel>>> GetInvoiceChangeLogsAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<InvoiceViewModel>>> GetAvailableInvoicesToLinkToCaseAsync(
            int? linkedInvoiceId, int? locationId, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> CreateInvoiceAsync(
            InvoiceEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateInvoiceAsync(
            InvoiceEditModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UpdateBasicInvoiceAsync(
            BasicInvoiceEditModel model, CancellationToken cancellationToken = default);
    }
}
