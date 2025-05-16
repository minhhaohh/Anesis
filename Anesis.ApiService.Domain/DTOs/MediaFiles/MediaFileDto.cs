namespace Anesis.ApiService.Domain.DTOs.MediaFiles
{
    public class MediaFileDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileExt { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public string Category { get; set; }

        public bool Signed { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string Notes { get; set; }
    }
}
