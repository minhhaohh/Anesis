using Anesis.Web.Data.Models;

namespace Anesis.Web.Data.Services
{
    public interface IMediaFileService
    {
        Task<ResponseModel<List<MediaFileViewModel>>> SearchMediaFilesAsync(
            MediaFileFilterModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<MediaFileViewModel>> GetMediaFileByIdAsync(
            int id, CancellationToken cancellationToken = default);

        Task<ResponseModel<SignatureViewModel>> GetSignatureFromMediaFileAsync(
            int fileId, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> SignToMediaFileAsync(
             SignatureViewModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> UploadMediaFileAsync(
             MediaFileUploadModel model, CancellationToken cancellationToken = default);

        Task<ResponseModel<string>> DeleteMediaFileAsync(
             int id, CancellationToken cancellationToken = default);
    }
}
