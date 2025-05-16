using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<ProposalViewModel>>> SearchProposalsAsync(
           ProposalFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<ProposalViewModel>>>(
                $"{API_Proposals}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<ProposalViewModel>> GetProposalByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<ProposalViewModel>>($"{API_Proposals}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Proposals}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateProposalAsync(
            ProposalEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Proposals}/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
