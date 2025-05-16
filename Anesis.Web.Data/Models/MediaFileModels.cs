using Anesis.Web.Data.DTO;
using Microsoft.AspNetCore.Components.Forms;

namespace Anesis.Web.Data.Models
{
    public class MediaFileFilterModel : PageableSearchModelBase
    {
        public string FileExt { get; set; }

        public string Category { get; set; }

        public bool NotSignedOnly { get; set; }
    }

    public class MediaFileViewModel
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

    public class SignatureViewModel
    {
        public int? Id { get; set; }

        public int FileId { get; set; }

        public string ImageBase64 { get; set; } 

        public byte[] Image { get; set; }

        public SignatureViewModel()
        {
            Image = Array.Empty<byte>();
        }
    }

    public class MediaFileUploadModel
    {
        public string Category { get; set; }

        public IBrowserFile File { get; set; }

        public string Notes { get; set; }
    }
}
