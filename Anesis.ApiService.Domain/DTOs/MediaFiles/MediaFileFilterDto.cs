using Anesis.ApiService.Domain.DTOs.Common;

namespace Anesis.ApiService.Domain.DTOs.MediaFiles
{
    public class MediaFileFilterDto : PageableSearchModelBase
    {
        public string FileExt { get; set; }

        public string Category { get; set; }

        public bool NotSignedOnly { get; set; }
    }
}
