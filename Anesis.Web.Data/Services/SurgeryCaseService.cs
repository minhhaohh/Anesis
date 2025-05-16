using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using Anesis.Web.Data.Models.Common;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        // SURGERY CASES
        public async Task<ResponseModel<List<SurgeryCaseViewModel>>> SearchSurgeryCasesAsync(
           SurgeryCaseFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<SurgeryCaseViewModel>>>(
                $"{API_Surgery_Cases}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<SurgeryCaseViewModel>> GetSurgeryCaseByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<SurgeryCaseViewModel>>($"{API_Surgery_Cases}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<List<ChangeLogViewModel>>> GetSurgeryCaseChangeLogsAsync(
           int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ChangeLogViewModel>>>(
                $"{API_Surgery_Cases}/ChangeLogs/{id}", cancellationToken);
        }

        public async Task<ResponseModel<string>> MarkSurgeryCaseAsCompletedAsync(
            int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/MarkAsCompleted/{id}", cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> MarkSurgeryCaseAsCancelledAsync(
            int id, string reason, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/MarkAsCancelled/{id}", reason, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> LinkInvoiceToSurgeryCaseAsync(
            LinkInvoiceCaseModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/LinkInvoice/{model.CaseId}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UnlinkInvoiceFromSurgeryCaseAsync(
            int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/UnlinkInvoice/{id}", cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        // SURGERY CASE CPT CODES
        public async Task<ResponseModel<List<CaseCptCodeViewModel>>> SearchCaseCptCodesAsync(
            CaseCptCodeFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CaseCptCodeViewModel>>>(
                $"{API_Surgery_Cases}/CaseCptCodes?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<string>> AddCaseCptCodeAsync(
            CaseCptCodeAddModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Surgery_Cases}/CaseCptCodes", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> ChangeCaseCptCodeQuantityAsync(
            int caseCostId, int newQuantity, CancellationToken cancellationToken = default)
        {
            var updateFieldModel = new FieldUpdateModel()
            {
                FieldName = nameof(CaseCptCodeViewModel.Quantity),
                NewValue = newQuantity.ToString()
            };

            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/CaseCptCodes/{caseCostId}", updateFieldModel, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        // SURGERY CASE DEVICES AND COSTS
        public async Task<ResponseModel<List<CaseCostViewModel>>> SearchCaseCostsAsync(
            CaseCostFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<CaseCostViewModel>>>(
                $"{API_Surgery_Cases}/CaseCosts?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<string>> AddCaseCostAsync(
            CaseCostAddModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Surgery_Cases}/CaseCosts", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> ChangeCaseCostQuantityAsync(
            int caseCostId, int newQuantity, CancellationToken cancellationToken = default)
        {
            var updateFieldModel = new FieldUpdateModel()
            {
                FieldName = nameof(CaseCostViewModel.Quantity),
                NewValue = newQuantity.ToString()
            };

            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/CaseCosts/{caseCostId}", updateFieldModel, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> ChangeCaseCostPriceAsync(
            int caseCostId, decimal newPrice, CancellationToken cancellationToken = default)
        {
            var updateFieldModel = new FieldUpdateModel()
            {
                FieldName = nameof(CaseCostViewModel.AppliedCost),
                NewValue = newPrice.ToString()
            };

            var response = await _httpClient.PatchAsJsonAsync($"{API_Surgery_Cases}/CaseCosts/{caseCostId}", updateFieldModel, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
