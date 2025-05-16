using Anesis.Web.Data.DTO;

namespace Anesis.Web.Data.Models
{
    public class LocationFilterModel : PageableSearchModelBase
    {
        public string LocationType { get; set; }

        public bool HasSiteId { get; set; }

        public bool ActiveOnly { get; set; }
    }

    public class LocationViewModel
    {
        public int Id { get; set; }

        public string ShortName { get; set; }

        public string LongName { get; set; }

        public string Code { get; set; }

        public string LocationType { get; set; }

        public bool IsActive { get; set; }

        public int? EcwSiteId { get; set; }

        public string BgColor { get; set; }

        public int? CountyId { get; set; }

        public string CountyName { get; set; }

        public string UmpquaMerchantId { get; set; }

        public string RectangleTerminalCode { get; set; }

        public string OpenEdgeTerminalCode { get; set; }
    }
}
