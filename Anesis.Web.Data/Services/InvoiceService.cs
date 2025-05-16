using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;
using System.Reflection;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<InvoiceViewModel>>> SearchInvoicesAsync(
            InvoiceFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<InvoiceViewModel>>>(
                $"{API_Invoices}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<InvoiceViewModel>> GetInvoiceByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<InvoiceViewModel>>($"{API_Invoices}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<List<ChangeLogViewModel>>> GetInvoiceChangeLogsAsync(
           int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ChangeLogViewModel>>>(
                $"{API_Invoices}/ChangeLogs/{id}", cancellationToken);
        }

        public async Task<ResponseModel<List<InvoiceViewModel>>> GetAvailableInvoicesToLinkToCaseAsync(
            int? linkedInvoiceId, int? locationId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<InvoiceViewModel>>>(
                $"{API_Invoices}/AvailableToLinkToCase?currentId={linkedInvoiceId}&locationId={locationId}", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateInvoiceAsync(
            InvoiceEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Invoices}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateInvoiceAsync(
            InvoiceEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Invoices}/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateBasicInvoiceAsync(
            BasicInvoiceEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Invoices}/Basic/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
