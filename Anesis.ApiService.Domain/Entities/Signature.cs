using Anesis.ApiService.Domain.Entities.Common;

namespace Anesis.ApiService.Domain.Entities
{
    public class Signature : IEntity
    {
        public int Id { get; set; }

        public int FileId { get; set; }

        public string ImageBase64 { get; set; }


        public CustomMediaFile MediaFile { get; set; }
    }
}
