using Anesis.ApiService.Domain.DTOs.Common;
using Anesis.ApiService.Domain.DTOs.MediaFiles;
using Anesis.ApiService.Domain.Entities;

namespace Anesis.ApiService.Services.IServices
{
    public interface IMediaFileService
    {
        Task<IPagedList<MediaFileDto>> SearchAsync(MediaFileFilterDto filter, 
            CancellationToken cancellationToken = default);

        Task<MediaFileDto> GetByIdAsync(int id, 
            CancellationToken cancellationToken = default);

        Task<SignatureDto> GetSignatureFromFileIdAsync(int fileId, 
            CancellationToken cancellationToken = default);

        Task<bool> UploadAsync(FileUploadDto model, 
            CancellationToken cancellationToken = default);

        Task<bool> SignToFileAsync(int fileId, string imageBase64, 
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int fileId, 
            CancellationToken cancellationToken = default);
    }
}
