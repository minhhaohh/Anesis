using Anesis.Web.Data.Models;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<MenuItemViewModel>>> GetMenuItemsForCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<MenuItemViewModel>>>($"{API_Menus}/MenuItems/ForCurrentUser", cancellationToken);
        }

        public async Task<ResponseModel<List<QuickLinkViewModel>>> GetQuickLinksForCurrentUserAsync(CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<QuickLinkViewModel>>>($"{API_Menus}/QuickLinks/ForCurrentUser", cancellationToken);
        }

        public async Task<ResponseModel<string>> CreateQuickLinkAsync(QuickLinkEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Menus}/QuickLinks", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UpdateQuickLinkAsync(QuickLinkEditModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsJsonAsync($"{API_Menus}/QuickLinks/{model.Id}", model, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> DeleteQuickLinkAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"{API_Menus}/QuickLinks/{id}", cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
