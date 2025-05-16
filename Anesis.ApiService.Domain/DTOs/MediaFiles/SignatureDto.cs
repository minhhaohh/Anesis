namespace Anesis.ApiService.Domain.DTOs.MediaFiles
{
    public class SignatureDto
    {
        public int Id { get; set; }

        public int FileId { get; set; }

        public string ImageBase64 { get; set; }

        public byte[] Image { get; set; }
    }
}
