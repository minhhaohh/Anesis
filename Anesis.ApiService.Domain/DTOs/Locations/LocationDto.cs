namespace Anesis.ApiService.Domain.DTOs.Locations
{
    public class LocationDto
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
