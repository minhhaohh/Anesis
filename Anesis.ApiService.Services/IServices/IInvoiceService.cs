using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.GenericInvoices;

namespace Anesis.ApiService.Services.IServices
{
    public interface IInvoiceService
    {
        Task<IPagedList<InvoiceDto>> SearchAsync(InvoiceFilterDto filter, 
            CancellationToken cancellationToken = default);

        Task<List<InvoiceDto>> GetAvailableInvoicesToLinkToCaseAsync(
            int? currentId, int? locationId, CancellationToken cancellationToken = default);

        Task<List<string>> GetAllOwnersAsync(
            CancellationToken cancellationToken = default);

        Task<InvoiceDto> GetByIdAsync(int id,
            bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<List<ChangeLogDto>> GetChangeLogsAsync(int id,
            CancellationToken cancellationToken = default);
    }
}
