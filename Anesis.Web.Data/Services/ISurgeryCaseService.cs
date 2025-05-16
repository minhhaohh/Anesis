using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface ISurgeryCaseService
    {
        // SURGERY CASES
        Task<ResponseModel<List<SurgeryCaseViewModel>>> SearchSurgeryCasesAsync(
            SurgeryCaseFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<SurgeryCaseViewModel>> GetSurgeryCaseByIdAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<List<ChangeLogViewModel>>> GetSurgeryCaseChangeLogsAsync(
           int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> MarkSurgeryCaseAsCompletedAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> MarkSurgeryCaseAsCancelledAsync(
            int id, string reason, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> LinkInvoiceToSurgeryCaseAsync(
            LinkInvoiceCaseModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UnlinkInvoiceFromSurgeryCaseAsync(
            int id, CancellationToken cancellationToken = default);


        // SURGERY CASE CPT CODES
        Task<ResponseModel<List<CaseCptCodeViewModel>>> SearchCaseCptCodesAsync(
            CaseCptCodeFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> AddCaseCptCodeAsync(
            CaseCptCodeAddModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ChangeCaseCptCodeQuantityAsync(
            int caseCptCodeId, int newQuantity, CancellationToken cancellationToken = default);


        // SURGERY CASE DEVICES AND COSTS
        Task<ResponseModel<List<CaseCostViewModel>>> SearchCaseCostsAsync(
            CaseCostFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> AddCaseCostAsync(
            CaseCostAddModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ChangeCaseCostQuantityAsync(
            int caseCostId, int newQuantity, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> ChangeCaseCostPriceAsync(
            int caseCostId, decimal newPrice, CancellationToken cancellationToken = default);
    }
}
