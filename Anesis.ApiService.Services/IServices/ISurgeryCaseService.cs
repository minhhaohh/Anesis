using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.GeneralChangeLogs;
using Anesis.ApiService.Domain.DTOs.SurgeryCases;

namespace Anesis.ApiService.Services.IServices
{
    public interface ISurgeryCaseService
    {
        Task<IPagedList<SurgeryCaseDto>> SearchAsync(SurgeryCaseFilterDto filter, CancellationToken cancellationToken = default);

        Task<SurgeryCaseDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> SetStatusAsync(CaseSetStatusDto model, CancellationToken cancellationToken = default);

        Task<bool> LinkInvoiceAsync(CaseLinkInvoiceDto model, CancellationToken cancellationToken = default);

        Task<bool> RemoveLinkedInvoiceAsync(int id, CancellationToken cancellationToken = default);

        Task<List<ChangeLogDto>> GetChangeLogsAsync(int id, CancellationToken cancellationToken = default);

        // CASE CPT CODES

        Task<IPagedList<CaseCptCodeDto>> SearchCaseCptCodesAsync(CaseCptCodeFilterDto filter, CancellationToken cancellationToken = default);

        Task<bool> UpdateCaseCptCodeFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default);

        Task<bool> DeleteCaseCptCodeAsync(int id, CancellationToken cancellationToken = default);


        // CASE COSTS
        Task<IPagedList<CaseCostDto>> SearchCaseCostsAsync(CaseCostFilterDto filter, CancellationToken cancellationToken = default);

        Task<bool> AddCaseCostAsync(CaseCostAddDto model, CancellationToken cancellationToken = default);

        Task<bool> UpdateCaseCostFieldAsync(FieldUpdateDto model, CancellationToken cancellationToken = default);

        Task<bool> DeleteCaseCostAsync(int id, CancellationToken cancellationToken = default);
    }
}
