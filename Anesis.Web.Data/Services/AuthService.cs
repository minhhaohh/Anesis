using Anesis.Web.Data.Models;
using System.Net;
using System.Net.Http.Json;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<LoginResponseModel>> LoginAsync(UserLoginModel model, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Auth}/Login", model, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ResponseModel<LoginResponseModel>>();
        }

        public async Task<ResponseModel<LoginResponseModel>> GetLoggedUserInfoAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"{API_Auth}/Info", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ResponseModel<LoginResponseModel>() { Success = false, Messages = new List<string> { "Unauthenticated" } };
            }

            return await response.Content.ReadFromJsonAsync<ResponseModel<LoginResponseModel>>();
        }

        public async Task<ResponseModel<bool>> LogoutAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"{API_Auth}/Logout", "", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ResponseModel<bool>>();
        }
    }
}
