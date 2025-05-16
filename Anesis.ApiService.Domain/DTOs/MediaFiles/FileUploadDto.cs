using Anesis.ApiService.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Anesis.ApiService.Domain.DTOs.MediaFiles
{
    public class FileUploadDto
    {
        public string Category { get; set; }

        public IFormFile File { get; set; }

        public string Notes { get; set; }

        public CustomMediaFile ToMediaFile()
        {
            return new CustomMediaFile()
            {
                FileName = File.FileName,
                FileExt = Path.GetExtension(File.FileName),
                FilePath = $"D:\\MediaFiles\\{File.FileName}",
                ContentType = File.ContentType,
                Category = Category,
                CreatedDate = DateTime.Now,
                CreatedBy = "haotm",
                Notes = Notes
            };
        }
    }
}
