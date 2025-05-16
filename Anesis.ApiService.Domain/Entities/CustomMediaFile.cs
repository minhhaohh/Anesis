using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class CustomMediaFile : IEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileExt { get; set; }

        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string Notes { get; set; }


        public List<Signature> Signatures { get; set; }
    }
}
