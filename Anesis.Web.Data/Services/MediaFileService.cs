using Anesis.Shared.Extensions;
using Anesis.Web.Data.Models;
using System.Net.Http.Json;
using System.Text;

namespace Anesis.Web.Data.Services
{
    public partial class ApiService
    {
        public async Task<ResponseModel<List<MediaFileViewModel>>> SearchMediaFilesAsync(
            MediaFileFilterModel model, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<List<MediaFileViewModel>>>(
                $"{API_Media_Files}?{model.ToQueryParams()}", cancellationToken);
        }

        public async Task<ResponseModel<MediaFileViewModel>> GetMediaFileByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<MediaFileViewModel>>(
                $"{API_Media_Files}/{id}", cancellationToken);
        }

        public async Task<ResponseModel<SignatureViewModel>> GetSignatureFromMediaFileAsync(
            int fileId, CancellationToken cancellationToken = default)
        {
            return await _httpClient.GetFromJsonAsync<ResponseModel<SignatureViewModel>>(
                $"{API_Media_Files}/SignatureFromFileId/{fileId}", cancellationToken);
        }

        public async Task<ResponseModel<string>> SignToMediaFileAsync(
             SignatureViewModel model, CancellationToken cancellationToken = default)
        {
            model.ImageBase64 = model.ImageBase64.IfNullOrWhiteSpace(Encoding.UTF8.GetString(model.Image));

            var response = await _httpClient.PostAsJsonAsync($"{API_Media_Files}/SignToFile/{model.FileId}",
                model.ImageBase64, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> UploadMediaFileAsync(
             MediaFileUploadModel model, CancellationToken cancellationToken = default)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Category), "Category");
            content.Add(new StringContent(model.Notes), "Notes");

            var fileContent = new StreamContent(model.File.OpenReadStream(maxAllowedSize: 1024 * 1024 * 15)); // max 15 MB
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.File.ContentType);
            content.Add(fileContent, "File", model.File.Name);

            var response = await _httpClient.PostAsync($"{API_Media_Files}/Upload", content, cancellationToken);
            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }

        public async Task<ResponseModel<string>> DeleteMediaFileAsync(
             int id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync(
                $"{API_Media_Files}/{id}", cancellationToken);

            return await response.Content.ReadFromJsonAsync<ResponseModel<string>>();
        }
    }
}
