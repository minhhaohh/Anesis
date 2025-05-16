using Anesis.ApiService.Infrastructures;
using Anesis.ApiService.Domain.DTOs.MediaFiles;
using Anesis.ApiService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomAuthorize]
    public class MediaFilesController : ControllerBase
    {
        private readonly ILogger<MediaFilesController> _logger;
        private readonly IMediaFileService _mediaService;

        public MediaFilesController(
            ILogger<MediaFilesController> logger,
            IMediaFileService mediaService)
        {
            _logger = logger;
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<Result> Search([FromQuery] MediaFileFilterDto filter)
        {
            var pagedData = await _mediaService.SearchAsync(filter);

            return Result.Ok(pagedData);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetById([FromRoute] int id)
        {
            var mediaFile = await _mediaService.GetByIdAsync(id);

            return mediaFile == null
                ? Result.Error($"Could not find media file with ID #{id}.")
                : Result.Ok(mediaFile);
        }

        [HttpGet("SignatureFromFileId/{fileId}")]
        public async Task<Result> GetSignatureFromFileId([FromRoute] int fileId)
        {
            var signature = await _mediaService.GetSignatureFromFileIdAsync(fileId);

            return signature == null 
                ? Result.Error($"Could not find signature from media file #${fileId}.")
                : Result.Ok(signature);
        }

        [HttpPost("Upload")]
        public async Task<Result> Upload([FromForm] FileUploadDto model)
        {
            if (!await _mediaService.UploadAsync(model))
            {
                return Result.Ok("Something went wrong when uploading media file. Please try again.");
            }

            return Result.Ok($"Uploaded new media file successful.");
        }

        [HttpPost("SignToFile/{id}")]
        public async Task<Result> SignToFile([FromRoute] int id, [FromBody] string imageBase64)
        {
            if (!await _mediaService.SignToFileAsync(id, imageBase64))
            {
                return Result.Ok($"Something went wrong when signing to media file #{id}. Please try again.");
            }

            return Result.Ok($"Signed to media file #{id} successful.");
        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] int id)
        {
            if (!await _mediaService.DeleteAsync(id))
            {
                return Result.Ok($"Something went wrong when deleting media file #{id}. Please try again.");
            }

            return Result.Ok($"Deleted media file #{id} successful.");
        }
    }
}
